using BloodBankCare.Data.Entity.Address;
using BloodBankCare.Data.Entity.ApplicationUsers;
using BloodBankCare.Data.Entity.MasterData;
using BloodBankCare.Data.Entity.SocialOrganizationInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Data.Entity.Bloodbank
{
    public class DonorInformation:Base
    {
        public string userId { get; set; }
        public string imageUrl { get; set; }
        public string designation { get; set; }
        public int? age { get; set; }
        public string nationalId { get; set; }

        public int? SocialOrganizationId { get; set; }
        public virtual SocialOrganization SocialOrganization { get; set; }

        public int? ProfessionId { get; set; }
        public virtual Profession Profession { get; set; }

        public int? ReligionId { get; set; }
        public virtual Religion Religion { get; set; }


        public string  ApplicationUserID { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<AddressInfo> AddressInfos { get; set; }

    }
}
