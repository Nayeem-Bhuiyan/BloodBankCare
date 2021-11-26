using BloodBankCare.Data.Entity.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Data.Entity.MasterData
{
    public class Thana:Base
    {
        public string thanaName { get; set; }
        public int? DistrictId { get; set; }
        public virtual District District { get; set; }

        public virtual IEnumerable<AddressInfo> AddressInfos { get; set; }
    }
}
