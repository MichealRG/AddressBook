using KsiazkaAdresowa.Controllers;
using KsiazkaAdresowa.Data;
using KsiazkaAdresowa.DTOs;
using KsiazkaAdresowa.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace AddressBookTest
{
    public class AddressBookControllerTests
    {
        private Mock<IDataRepository> _dataRepoMock;
        private Mock<IDtoServiceInterface> _dtoServMock;
        private Mock<ILogger<AddressBookController>> _dataLogger;
        private AddressBookController _abc;

        public AddressBookControllerTests()
        {
            _dataRepoMock = new Mock<IDataRepository>();
            _dtoServMock = new Mock<IDtoServiceInterface>();
            _dataLogger = new Mock<ILogger<AddressBookController>>();
            _abc = new AddressBookController(_dataRepoMock.Object, _dtoServMock.Object, _dataLogger.Object);
        }
        [Fact]
        public async Task TestGetUserByLogin_ReturnsAddressDtoObject()
        {
            //Arrange
            var tele = new TeleAddress { Country = 0, Home = "a", TeleAddressId = 1, NumberOfBuilding = 1, Street = "a", NumberOfLocal = 1, PersonId = 1, Post = "a", PostCode = "33-333" };
            var cont = new ContactData { PersonId = 1, ContactDataId = 1, Email = "a@a", PhoneNumber = "999888777" };
            var person = new Person { FirstName = "a", Surname = "b", Login = "aaa", TimeOfAdding = DateTime.Now, PersonId = 1, TypeOfAppliciant = 0, ContactData=cont,TeleAddressData=tele };
            var dto = new AddressDto { FirstName = "a", Surname = "b", Login = "aaa", TypeOfAppliciant = 0, Street = "a", Country = 0, Email = "a@a", Home = "a", NumberOfBuilding = 1, NumberOfLocal = 1, PhoneNumber = "999888777", Post = "a", PostCode = "33-333" };
            _dataRepoMock.Setup(x => x.GetUser("aaa")).ReturnsAsync(person);
            _dtoServMock.Setup(x => x.PersonIntoAddressDto(person))
                .Returns(dto);
            //Act
            var result= await _abc.GetMember("aaa");
            var parsedObject=result.Result as OkObjectResult;
            ///Assert
            Assert.NotNull(parsedObject.Value);

            Assert.Equal("aaa", ((AddressDto)parsedObject.Value).Login);
        }
        [Fact]
        public async Task TestGetLastAddedPerson_ReturnJustAddedPerson()
        {
            //Arrange
            var tele = new TeleAddress { Country = 0, Home = "a", TeleAddressId = 1, NumberOfBuilding = 1, Street = "a", NumberOfLocal = 1, PersonId = 1, Post = "a", PostCode = "33-333" };
            var cont = new ContactData { PersonId = 1, ContactDataId = 1, Email = "a@a", PhoneNumber = "999888777" };
            var person = new Person { FirstName = "a", Surname = "b", Login = "aaa", TimeOfAdding = DateTime.Now, PersonId = 1, TypeOfAppliciant = 0, ContactData = cont, TeleAddressData = tele };
            var dto = new AddressDto { FirstName = "a", Surname = "b", Login = "aaa", TypeOfAppliciant = 0, Street = "a", Country = 0, Email = "a@a", Home = "a", NumberOfBuilding = 1, NumberOfLocal = 1, PhoneNumber = "999888777", Post = "a", PostCode = "33-333" };
            
            _dataRepoMock.Setup(x => x.GetLastAddedPerson()).ReturnsAsync(person);
            _dtoServMock.Setup(x => x.PersonIntoAddressDto(person))
                .Returns(dto);

            //Act
            var result = _abc.GetLastAddedPerson().Result.Result as OkObjectResult;

            //Assert
            Assert.Equal((AddressDto)result.Value, dto);

        }
        [Fact]
        public async Task AddNewMemberToDb_ReturnNewObjectOfPerson()
        {
            var tele = new TeleAddress { Country = 0, Home = "a", TeleAddressId = 0, NumberOfBuilding = 1, Street = "a", NumberOfLocal = 1, PersonId = 0, Post = "a", PostCode = "33-333" };
            var cont = new ContactData { PersonId = 0, ContactDataId = 0, Email = "a@a", PhoneNumber = "999888777" };
            var person = new Person { FirstName = "a", Surname = "b", Login = "aaa", TimeOfAdding = DateTime.Now, PersonId = 0, TypeOfAppliciant = 0, ContactData = cont, TeleAddressData = tele };
            var dto = new AddressDto { FirstName = "a", Surname = "b", Login = "aaa", TypeOfAppliciant = 0, Street = "a", Country = 0, Email = "a@a", Home = "a", NumberOfBuilding = 1, NumberOfLocal = 1, PhoneNumber = "999888777", Post = "a", PostCode = "33-333" };



            var result = await _abc.AddMember(dto);

            Assert.Equal(person, result.Value);

        }
    }
}
