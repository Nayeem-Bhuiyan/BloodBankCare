using BloodBankCare.Areas.Auth.Models;
using BloodBankCare.Data.Entity.Address;
using BloodBankCare.Data.Entity.ApplicationUsers;
using BloodBankCare.Data.Entity.Bloodbank;
using BloodBankCare.Data.Entity.MasterData;
using BloodBankCare.Data.Entity.SocialOrganizationInfo;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Models
{
    public class UserDashBoardViewModel
    {
        //----------------User Information-----------------------------------------------
        public string ApplicationUserId { get; set; }
        public string Name { get; set; }//User Name
        public string Email { get; set; }
        public string RoleId { get; set; }
        public string PhoneNumber { get; set; }
        public int? userTypeId { get; set; }
        public IFormFile ImgeUrl { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public bool RememberMe { get; set; }

        public int? GenderId { get; set; }
        public virtual Gender Gender { get; set; }

        public int? BloodGroupId { get; set; }
        public virtual BloodGroup BloodGroup { get; set; }

        public IEnumerable<ApplicationRoleViewModel> userRoles { get; set; }
        public IEnumerable<UserType> userTypes { get; set; }
        public IEnumerable<AspNetUsersViewModel> aspNetUsers { get; set; }
        public IEnumerable<BloodGroup> bloodGroups { get; set; }
        public IEnumerable<Gender> genders { get; set; }


        //--------------------------Donor Information---------------------
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
        public int PresentAddressId { get; set; }
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
        public virtual IEnumerable<DonorInformation> donorInformations { get; set; }
        public virtual IEnumerable<ApplicationUser> donorInformationWithUserDetails { get; set; }
        public virtual IEnumerable<Religion> religions { get; set; }
        public virtual IEnumerable<Profession> professions { get; set; }
        public virtual IEnumerable<SocialOrganization> socialOrganizations { get; set; }
        public virtual IEnumerable<Country> countries { get; set; }
        public virtual IEnumerable<District> districts { get; set; }
        public virtual IEnumerable<Thana>thanas { get; set; }



        //--------------Dash Board Information------------------------------------

        public int? LastDonationDayCount { get; set; }
        public int? TotalDonation { get; set; }
        public virtual DonorInformation donorInformation { get; set; }
        public virtual ApplicationUser applicationUser { get; set; }
        public virtual IEnumerable<AddressInfo> addressInfos { get; set; }
        public virtual IEnumerable<Data.Entity.Bloodbank.DonationRecordInfo> DonationRecordInfos { get; set; }
        public virtual IEnumerable<Data.Entity.Bloodbank.DonationRecordInfo> BloodReceivedInfos { get; set; }
        public virtual IEnumerable<BloodRequestInfo> BloodRequestInfos { get; set; }
        public virtual AddressInfo presentAddress { get; set; }
        public virtual AddressInfo permanentAddress { get; set; }
        public virtual Data.Entity.Bloodbank.DonationRecordInfo donationRecord { get; set; }


    }
}
