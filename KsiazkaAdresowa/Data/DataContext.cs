using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KsiazkaAdresowa.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .HasOne<ContactData>(p => p.ContactData)
                .WithOne(cd => cd.PersonSource)
                .HasForeignKey<ContactData>(cd => cd.PersonId);
            modelBuilder.Entity<Person>()
                .HasOne<TeleAddress>(p => p.TeleAddressData)
                .WithOne(ta => ta.PersonSource)
                .HasForeignKey<TeleAddress>(ta => ta.PersonId).OnDelete(DeleteBehavior.Restrict);
        }
        public DbSet<Person> Persons { get; set; }
        public DbSet<ContactData> ContactsData { get; set; }
        public DbSet<TeleAddress> TeleAddresses { get; set; }
    }
}
