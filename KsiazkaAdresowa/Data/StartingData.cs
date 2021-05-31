using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KsiazkaAdresowa.Data
{
    public class StartingData
    {
        public static void Initialize(IServiceProvider services)
        {
            using (var context = new DataContext(services.GetRequiredService<DbContextOptions<DataContext>>()))
            {
                if (context.Persons.Count() > 0)
                    return;
                context.Persons.AddRange(
                    new Person
                    {
                        PersonId = 1,
                        ContactData = new ContactData()
                        {
                            ContactDataId = 1,
                            Email = "a@a",
                            PersonId = 1,
                            PhoneNumber = "555222111"
                        },
                        FirstName = "a",
                        Login = "ab",
                        Surname = "b",
                        TimeOfAdding = DateTime.Now,
                        TeleAddressData = new TeleAddress()
                        {
                            Country = Country.German,
                            Street = "bc",
                            Home = "papa",
                            TeleAddressId = 1,
                            NumberOfBuilding = 1,
                            NumberOfLocal = 1,
                            PersonId = 1,
                            Post = "cd",
                            PostCode = "33-333"
                        },
                        TypeOfAppliciant = Appliciant.NaturalPerson
                    },
                    new Person
                    {
                        PersonId = 2,
                        ContactData = new ContactData()
                        {
                            ContactDataId = 2,
                            Email = "b@b",
                            PersonId = 2,
                            PhoneNumber = "222343565"
                        },
                        FirstName = "b",
                        Login = "cb",
                        Surname = "c",
                        TimeOfAdding = DateTime.Now,
                        TeleAddressData = new TeleAddress()
                        {
                            Country = Country.Poland,
                            Street = "vf",
                            Home = "papa",
                            TeleAddressId = 2,
                            NumberOfBuilding = 2,
                            NumberOfLocal = 3,
                            PersonId = 2,
                            Post = "cd",
                            PostCode = "33-333"
                        },
                        TypeOfAppliciant = Appliciant.NaturalPerson
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
