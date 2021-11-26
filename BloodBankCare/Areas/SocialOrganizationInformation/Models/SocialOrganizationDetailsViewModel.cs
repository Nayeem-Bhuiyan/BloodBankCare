using BloodBankCare.Data.Entity.SocialOrganizationInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Areas.SocialOrganizationInformation.Models
{
    public class SocialOrganizationDetailsViewModel
    {
        public int SocialOrganizationDetailsId { get; set; }
        public string organizationTypeName { get; set; }  //blood,human service,child service etc
        public string location { get; set; }
        public DateTime? establishedYear { get; set; }
        public string presentPresidentName { get; set; }
        public string creator { get; set; }
        public string contactNo { get; set; }
        //fk
        public int? SocialOrganizationId { get; set; }

        public virtual SocialOrganizationDetails socialOrganizationDetails { get; set; }
        public virtual IEnumerable<SocialOrganizationDetails> socialOrganizationDetailsList { get; set; }
        public virtual IEnumerable<SocialOrganization> socialOrganizations { get; set; }
    }
}
