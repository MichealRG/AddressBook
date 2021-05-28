using KsiazkaAdresowa.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KsiazkaAdresowa.Data
{
    public class DataRepository: IDataRepository
    {
        private readonly DataContext _context;

        public DataRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Person> GetLastAddedPerson()
        {
            return  await _context.Persons
                .Include(x => x.ContactData)
                .Include(x => x.TeleAddressData)
                .OrderByDescending(x => x.TimeOfAdding)
                .Take(1).FirstOrDefaultAsync();
        }

        public async Task<Person> GetUser(string login)
        {
            return await _context.Persons
                .Include(x => x.ContactData)
                .Include(x => x.TeleAddressData)
                .FirstOrDefaultAsync(x => x.Login == login);
        }

        public async Task<IEnumerable<Person>> GetUsers()
        {
            return await _context.Persons
                .Include(x => x.ContactData)
                .Include(x => x.TeleAddressData)
                .ToListAsync();
        }

        public async Task<IEnumerable<Person>> GetUsersByCity(string city)
        {
            return await _context.Persons
                .Include(x => x.ContactData)
                .Include(x => x.TeleAddressData)
                .Where(x=>x.TeleAddressData.Home==city)
                .ToListAsync();
        }
    }
}
