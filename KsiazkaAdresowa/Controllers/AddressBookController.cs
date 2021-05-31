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
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using KsiazkaAdresowa.Logging;

namespace KsiazkaAdresowa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressBookController : Controller
    {
        private readonly IDataRepository _repository;
        private readonly IDtoServiceInterface _dtoService;
        private readonly ILogger<AddressBookController> _logger;

        public AddressBookController( IDataRepository repository, IDtoServiceInterface dtoServices, ILogger<AddressBookController> logger)
        {
            _repository = repository;
            _dtoService = dtoServices;
            _logger = logger;
        }
        [HttpGet("Member/lastadded")]
        public async Task<ActionResult<AddressDto>> GetLastAddedPerson()
        {
            _logger.LogInformation(MyLogEvents.GetItem, "Getting item last added item");
            var person = await _repository.GetLastAddedPerson();
            if (person == null) {
                _logger.LogInformation(MyLogEvents.GetItem, "Getting item last added item - not found any item");
                return NotFound();
            }
            _logger.LogInformation(MyLogEvents.GetItem, "Getting item last added item, succeed");
            return Ok(_dtoService.PersonIntoAddressDto(person));
        }
        [HttpGet("Members/city/{city}")]
        public async Task<ActionResult<IEnumerable<AddressDto>>> GetPeopleFromCity(string city)
        {
            _logger.LogInformation(MyLogEvents.GetItem, "Getting people by city, they live in");
            var people = await _repository.GetUsersByCity(city);
            if (people.Count() == 0)
            {
                _logger.LogWarning(MyLogEvents.GetItem, "Getting people by city - not found any person");
                return NotFound();
            }
            _logger.LogInformation(MyLogEvents.GetItem, "Getting people by city, they live in, succeed");
            return Ok(_dtoService.PersonsIntoAddressesDto(people));
        }
        [HttpGet("Member/login/{login}")]
        public async Task<ActionResult<AddressDto>> GetMember(string login)
        {
            _logger.LogInformation(MyLogEvents.GetItem, "Downloading person by login");
            var member = await _repository.GetUser(login);
            if (member == null)
            {
                _logger.LogWarning(MyLogEvents.GetItemNotFound, "Downloading person by login, failed");
                return NotFound();
            }
            _logger.LogInformation(MyLogEvents.GetItem, "Downloading person by login, succeed");
            var x = _dtoService.PersonIntoAddressDto(member);
            return Ok(x);
        }
        [HttpGet("Members")]
        public async Task<ActionResult<IEnumerable<AddressDto>>> GetMembers()
        {
            _logger.LogInformation(MyLogEvents.ListItems, "Downloading all data from db");
            var people = await _repository.GetUsers();
           if (people.Count() == 0)
            {
                _logger.LogWarning(MyLogEvents.GetItemNotFound, "Downloading data from db, failed");
                return NotFound();
            }
            _logger.LogInformation(MyLogEvents.ListItems, "Downloading data from db, succeed");
            return Ok(_dtoService.PersonsIntoAddressesDto(people));
        }
        [HttpPost]
        public async Task<ActionResult<Person>> AddMember(AddressDto address)
        {
            _logger.LogInformation(MyLogEvents.InsertItem, "Adding item into db and file");

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
                await _repository.AddMemberToDb(member);
                //await SaveToFile(member);
                _logger.LogInformation(MyLogEvents.InsertItem, "Adding item into db and file, succeed");
                return member;
            }
            _logger.LogWarning(MyLogEvents.InsertItem, "Adding item into db and file, occured error (ModelState wasn't valid)");
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
            using StreamWriter file = new("AddressBook\\Data.txt");
            await file.WriteLineAsync(lineToSave);
        }
    }
}
