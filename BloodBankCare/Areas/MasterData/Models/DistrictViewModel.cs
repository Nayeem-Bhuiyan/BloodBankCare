using BloodBankCare.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Areas.MasterData.Models
{
    public class DistrictViewModel
    {
        public int DistrictId { get; set; }
        public string districtName { get; set; }
        //fk
        public int? CountryId { get; set; }


        public virtual District district { get; set; }
        public virtual IEnumerable<District> districts { get; set; }
        public virtual IEnumerable<Country> countries { get; set; }
    }
}
