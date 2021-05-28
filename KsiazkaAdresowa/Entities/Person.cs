using System;

namespace KsiazkaAdresowa.Data
{
    public enum Appliciant
    {
        NaturalPerson,
        Company
    }
    public class Person
    {
        public int PersonId { get; set; }
        public Appliciant TypeOfAppliciant { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Login { get; set; }
        public DateTime TimeOfAdding { get; set; }
        public virtual TeleAddress TeleAddressData { get; set; }
        public virtual ContactData ContactData { get; set; }
    }
}