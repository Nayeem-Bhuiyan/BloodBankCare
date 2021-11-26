using BloodBankCare.Data.Entity.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Data.Entity.MasterData
{
    public class Country:Base
    {
        public string countryName { get; set; }
        public virtual IEnumerable<District> districts { get; set; }
        public virtual IEnumerable<AddressInfo> AddressInfos { get; set; }
    }
}
