using KsiazkaAdresowa.Data;
using KsiazkaAdresowa.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KsiazkaAdresowa.Interfaces
{
    public interface IDtoServiceInterface
    {
        public AddressDto PersonIntoAddressDto(Person person);
        public IEnumerable<AddressDto> PersonsIntoAddressesDto(IEnumerable<Person> people);
    }
}
