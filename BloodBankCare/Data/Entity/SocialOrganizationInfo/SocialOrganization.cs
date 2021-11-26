using BloodBankCare.Data.Entity.Bloodbank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Data.Entity.SocialOrganizationInfo
{
    public class SocialOrganization : Base
    {
        public string socialOrgName { get; set; }

        public virtual IEnumerable<SocialOrganizationBrunch> SocialOrganizationBrunches { get; set; }
        public virtual IEnumerable<SocialOrganizationDetails> SocialOrganizationDetails { get; set; }
        public virtual IEnumerable<DonorInformation> DonorInformations { get; set; }
    }
}
