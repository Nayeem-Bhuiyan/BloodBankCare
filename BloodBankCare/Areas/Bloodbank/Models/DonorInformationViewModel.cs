using BloodBankCare.Data.Entity.ApplicationUsers;
using BloodBankCare.Data.Entity.Bloodbank;
using BloodBankCare.Data.Entity.MasterData;
using BloodBankCare.Data.Entity.SocialOrganizationInfo;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Areas.Bloodbank.Models
{
    public class DonorInformationViewModel
    {
        public int DonorInformationId { get; set; }
        public string userId { get; set; }  //collect in controller
        public string imageUrl { get; set; }
        public IFormFile DonorImage { get; set; }  //form control name
        public string designation { get; set; }
        public int? age { get; set; }
        public string nationalId { get; set; }

        //fk
        public int? SocialOrganizationId { get; set; }
        public int? ProfessionId { get; set; }
        public int? ReligionId { get; set; }




        //addressTable Info
        //public string userId { get; set; }
        public int PresentAddressId { get; set; }
        public int PermanentAddressId { get; set; }
        public string careOf { get; set; }
        public string houseNo { get; set; }
        public string villageName { get; set; }
        public string postOffice { get; set; }

        public int? CountryId { get; set; }
        public virtual Country Country { get; set; }

        public int? DistrictId { get; set; }
        public virtual District District { get; set; }

        public int? ThanaId { get; set; }
        public virtual Thana Thana { get; set; }


        //Present Address
        public string PresentcareOf { get; set; }
        public string PresenthouseNo { get; set; }
        public string PresentvillageName { get; set; }
        public string PresentpostOffice { get; set; }
        public int? addressType { get; set; }  //present Or Permanent(checkBox)

        public int? PresentCountryId { get; set; }
        public int? PresentDistrictId { get; set; }
        public int? PresentThanaId { get; set; }





        //public int? DonorInformationId { get; set; }
        //public virtual DonorInformation DonorInformation { get; set; }

        //navigation property
        public virtual DonorInformation donorInformation { get; set; }
        public virtual IEnumerable<DonorInformation> donorInformations { get; set; }
        public virtual IEnumerable<ApplicationUser> donorInformationWithUserDetails { get; set; }
        public virtual IEnumerable<Religion> religions { get; set; }
        public virtual IEnumerable<Profession> professions { get; set; }
        public virtual IEnumerable<SocialOrganization> socialOrganizations { get; set; }
        public virtual IEnumerable<Country> countries { get; set; }
    }
}
