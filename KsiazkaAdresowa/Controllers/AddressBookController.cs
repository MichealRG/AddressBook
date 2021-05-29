using KsiazkaAdresowa.Data;
using KsiazkaAdresowa.DTOs;
using KsiazkaAdresowa.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

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
        public async Task<ActionResult<IEnumerable<AddressDto>>> GetPeopleFromCity(string city)
        {
            var people = await _repository.GetUsersByCity(city);
            if (people.Count() == 0)
                return NotFound();
            return Ok(_dtoService.PersonsIntoAddressesDto(people));
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
                await SaveToFile(member);
                return member;
            }
            return BadRequest("This was not a good reuest");
        }

        private async Task SaveToFile(Person member)
        {
            string lineToSave = $"Type of application: {member.TypeOfAppliciant},\n" +
                $"First name: {member.FirstName},\n" +
                $"Last name: {member.Surname}\n" +
                $"Login: {member.Login}\n" +
                $"Time of adding: {member.TimeOfAdding}\n" +
                $"Phone number: {member.ContactData.PhoneNumber}\n" +
                $"Email: {member.ContactData.Email}\n" +
                $"City: {member.TeleAddressData.Home}\n" +
                $"Street: {member.TeleAddressData.Street}, number: {member.TeleAddressData.NumberOfBuilding}, {member.TeleAddressData.NumberOfLocal}\n" +
                $"Post office: {member.TeleAddressData.Post}, post code: {member.TeleAddressData.PostCode}\n" +
                $"Country: {member.TeleAddressData.Country}\n" +
                $"--------";
            using StreamWriter file = new("AddressBook\\Data.txt", append: true);
            await file.WriteLineAsync(lineToSave);
        }
    }
}
