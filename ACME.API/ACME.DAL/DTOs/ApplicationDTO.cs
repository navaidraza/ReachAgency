namespace ACME.DAL.DTOS
{
    public class ApplicationDTO
    {
        public CountryDTO country { get; set; }
        public string state { get; set; }
        public string postCode { get; set; }
        public int postCodeId { get; set; }
        public string fullName { get; set; }
    }
}
