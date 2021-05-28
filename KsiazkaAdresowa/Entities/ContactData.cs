namespace KsiazkaAdresowa.Data
{
    public class ContactData
    {
        public int ContactDataId { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int PersonId { get; set; }
        public virtual Person PersonSource { get; set; }
    }
}