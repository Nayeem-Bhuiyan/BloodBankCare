using BloodBankCare.Data.Entity.SocialOrganizationInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Areas.SocialOrganizationInformation.Models
{
    public class SocialOrganizationViewModel
    {
        public int SocialOrganizationId { get; set; }
        public string socialOrgName { get; set; }


        public virtual SocialOrganization socialOrganization { get; set; }
        public virtual IEnumerable<SocialOrganization> socialOrganizations { get; set; }
    }
}
