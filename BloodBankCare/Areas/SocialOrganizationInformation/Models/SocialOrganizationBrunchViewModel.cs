
using BloodBankCare.Data.Entity.MasterData;
using BloodBankCare.Data.Entity.SocialOrganizationInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Areas.SocialOrganizationInformation.Models
{
    public class SocialOrganizationBrunchViewModel
    {
        public int SocialOrganizationBrunchId { get; set; }
        public string brunchName { get; set; }
        public string brunchLocation { get; set; }

        public int? SocialOrganizationId { get; set; }


        public virtual SocialOrganizationBrunch SocialOrganizationBrunch { get; set; }
        public virtual IEnumerable<SocialOrganizationBrunch> SocialOrganizationBrunchs { get; set; }
        public virtual IEnumerable<SocialOrganization> socialOrganizations { get; set; }
    }
}
