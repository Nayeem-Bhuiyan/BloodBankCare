using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Data.Entity.SocialOrganizationInfo
{
    public class SocialOrganizationDetails : Base
    {
        public string organizationTypeName { get; set; }  //blood,human service,child service etc
        public string location { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? establishedYear { get; set; }
        public string presentPresidentName { get; set; }
        public string creator { get; set; }
        public string contactNo { get; set; }

        //fk
        public int? SocialOrganizationId { get; set; }
        public virtual SocialOrganization SocialOrganization { get; set; }

    }
}
