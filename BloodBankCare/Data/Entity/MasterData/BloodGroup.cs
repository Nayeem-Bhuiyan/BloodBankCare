using BloodBankCare.Data.Entity.ApplicationUsers;
using BloodBankCare.Data.Entity.Bloodbank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Data.Entity.MasterData
{
    public class BloodGroup:Base
    {
        public string groupName { get; set; }

        public virtual IEnumerable<BloodRequestInfo> BloodRequestInfos { get; set; }
        public virtual IEnumerable<ApplicationUser> ApplicationUsers { get; set; }
        public virtual IEnumerable<DonationRecordInfo> DonationRecordInfos { get; set; }
    }
}
