using KsiazkaAdresowa.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KsiazkaAdresowa.Interfaces
{
    public interface IDataRepository
    {
        public Task<IEnumerable<Person>> GetUsers();
        public Task<Person> GetUser(string login);
        public Task<IEnumerable<Person>> GetUsersByCity(string city);
        public Task<Person> GetLastAddedPerson();
        public Task AddMemberToDb(Person person);


    }
}
