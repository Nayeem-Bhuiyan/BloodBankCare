using BloodBankCare.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Areas.MasterData.Models
{
    public class CountryViewModel
    {
        public int CountryId { get; set; }
        public string countryName { get; set; }


        public virtual Country country { get; set; }
        public virtual IEnumerable<Country> countries { get; set; }
    }
}
