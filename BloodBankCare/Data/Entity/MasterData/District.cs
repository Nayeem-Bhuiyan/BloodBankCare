using BloodBankCare.Data.Entity.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Data.Entity.MasterData
{
    public class District:Base
    {
        public string districtName { get; set; }
        public int?  CountryId { get; set; }
        public virtual Country Country { get; set; }
        public virtual IEnumerable<Thana> Thanas { get; set; }

        public virtual IEnumerable<AddressInfo> AddressInfos { get; set; }
    }
}
