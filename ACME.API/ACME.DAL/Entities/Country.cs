using System;
using System.Collections.Generic;

namespace ACME.DAL
{
    public partial class Country
    {
        public Country()
        {
            Applications = new HashSet<Applications>();
        }

        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }

        public virtual ICollection<Applications> Applications { get; set; }
    }
}
