using KsiazkaAdresowa.Data;
using KsiazkaAdresowa.DTOs;
using KsiazkaAdresowa.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KsiazkaAdresowa.Services
{
    public class DtoService : IDtoServiceInterface
    {
        public AddressDto PersonIntoAddressDto(Person person)
        {
            return new AddressDto()
            {
                PhoneNumber = person.ContactData.PhoneNumber,
                Post = person.TeleAddressData.Post,
                PostCode = person.TeleAddressData.PostCode,
                Country = person.TeleAddressData.Country,
                Email = person.ContactData.Email,
                FirstName = person.FirstName,
                Home = person.TeleAddressData.Home,
                Login = person.Login,
                NumberOfBuilding = person.TeleAddressData.NumberOfBuilding,
                TypeOfAppliciant = person.TypeOfAppliciant,
                NumberOfLocal = person.TeleAddressData.NumberOfLocal,
                Street = person.TeleAddressData.Street,
                Surname = person.Surname
            };
        }

        public IEnumerable<AddressDto> PersonsIntoAddressesDto(IEnumerable<Person> people)
        {
            List<AddressDto> addressesList = new();
            foreach (var person in people)
            {
                addressesList.Add(new AddressDto()
                {
                    PhoneNumber = person.ContactData.PhoneNumber,
                    Post = person.TeleAddressData.Post,
                    PostCode = person.TeleAddressData.PostCode,
                    Country = person.TeleAddressData.Country,
                    Email = person.ContactData.Email,
                    FirstName = person.FirstName,
                    Home = person.TeleAddressData.Home,
                    Login = person.Login,
                    NumberOfBuilding = person.TeleAddressData.NumberOfBuilding,
                    TypeOfAppliciant = person.TypeOfAppliciant,
                    NumberOfLocal = person.TeleAddressData.NumberOfLocal,
                    Street = person.TeleAddressData.Street,
                    Surname = person.Surname
                });
            }
            return addressesList;
        }
    }
}
