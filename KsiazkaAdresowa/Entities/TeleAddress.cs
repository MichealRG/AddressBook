namespace KsiazkaAdresowa.Data
{
    public enum Country
    {
        UnitedKingdom,
        Poland,
        German,
        UnitedStates
    }
    public class TeleAddress
    {
        public int TeleAddressId { get; set; }
        public string Street { get; set; }
        public int NumberOfBuilding { get; set; }
        public int NumberOfLocal { get; set; }
        public string Home { get; set; }
        public string Post { get; set; }
        public string PostCode { get; set; }
        public Country Country { get; set; }
        public int PersonId { get; set; }
        public virtual Person PersonSource { get; set; }
    }
}