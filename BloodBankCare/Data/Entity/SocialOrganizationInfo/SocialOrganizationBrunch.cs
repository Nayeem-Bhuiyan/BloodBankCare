using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Data.Entity.SocialOrganizationInfo
{
    public class SocialOrganizationBrunch : Base
    {
        public string brunchName { get; set; }
        public string brunchLocation { get; set; }
        //fk
        public int? SocialOrganizationId { get; set; }
        public virtual SocialOrganization SocialOrganization { get; set; }

    }
}
