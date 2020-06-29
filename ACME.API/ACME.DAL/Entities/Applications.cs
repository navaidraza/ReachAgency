using System;

namespace ACME.DAL
{
    public partial class Applications
    {
        public int ApplicationId { get; set; }
        public int CountryId { get; set; }
        public string State { get; set; }
        public int? PostcodeId { get; set; }
        public string FullName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public virtual Country Country { get; set; }
        public virtual Postcodes Postcode { get; set; }
    }
}
