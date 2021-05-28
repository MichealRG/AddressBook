using KsiazkaAdresowa.Data;
using KsiazkaAdresowa.DTOs;
using KsiazkaAdresowa.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KsiazkaAdresowa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressBookController : Controller
    {
        private readonly DataContext _context;
        private readonly IDataRepository _repository;
        private readonly IDtoServiceInterface _dtoService;

        public AddressBookController(DataContext context, IDataRepository repository, IDtoServiceInterface dtoServices )
        {
            _context = context;
            _repository = repository;
            _dtoService = dtoServices;
        }
        [HttpGet("Member/lastadded")]
        public async Task<ActionResult<AddressDto>> GetLastAddedPerson()
        {
            return Ok(_dtoService.PersonIntoAddressDto(await _repository.GetLastAddedPerson()));
        }
        [HttpGet("Members/city/{city}")]
        public ActionResult<IEnumerable<AddressDto>> GetPeopleFromCity(string city)
        {
            return Ok(_dtoService.PersonsIntoAddressesDto(_repository.GetUsersByCity(city).Result));
        }
        [HttpGet("Member/login/{login}")]
        public async Task<ActionResult<AddressDto>> GetMember(string login)
        {
            var member = await _repository.GetUser(login);
            if (member == null)
                return NotFound();
            return Ok(_dtoService.PersonIntoAddressDto(member));
        }
        [HttpGet("Members")]
        public ActionResult<IEnumerable<AddressDto>> GetMembers()
        {
            return Ok(_dtoService.PersonsIntoAddressesDto(_repository.GetUsers().Result));
        }
        [HttpPost]
        public async Task<ActionResult<Person>> AddMember(AddressDto address)
        {
            if (ModelState.IsValid)
            {
                var member = new Person
                {
                    Surname = address.Surname,
                    FirstName = address.FirstName,
                    Login = address.Login,
                    TypeOfAppliciant = address.TypeOfAppliciant,
                    TimeOfAdding = DateTime.Now,
                    TeleAddressData = new TeleAddress
                    {
                        Country = address.Country,
                        Home = address.Home,
                        NumberOfBuilding = address.NumberOfBuilding,
                        NumberOfLocal = address.NumberOfLocal,
                        Post = address.Post,
                        PostCode = address.PostCode,
                        Street = address.Street
                    },
                    ContactData = new ContactData
                    {
                        Email = address.Email,
                        PhoneNumber = address.PhoneNumber
                    }
                };
                _context.Add(member);
                await _context.SaveChangesAsync();
                return member;
            }
            return BadRequest("This was not a good reuest");
        }
    }
}
