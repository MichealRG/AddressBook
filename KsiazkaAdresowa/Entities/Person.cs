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
        public override bool Equals(object obj)
        {

            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Person parsedObj = (Person)obj;
                return parsedObj.ContactData != null && parsedObj.FirstName == FirstName && parsedObj.Login == Login &&
                    parsedObj.PersonId == PersonId && parsedObj.Surname == Surname && parsedObj.TeleAddressData != null &&
                    parsedObj.TypeOfAppliciant == TypeOfAppliciant;
            }
        }
    }
}