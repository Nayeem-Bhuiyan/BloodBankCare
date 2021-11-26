using BloodBankCare.Data.Entity.Address;
using BloodBankCare.Data.Entity.ApplicationUsers;
using BloodBankCare.Data.Entity.Bloodbank;
using BloodBankCare.Models;
using BloodBankCare.Services.AddressInfoService.Interfaces;
using BloodBankCare.Services.AuthService.Interfaces;
using BloodBankCare.Services.BloodbankService.Interfaces;
using BloodBankCare.Services.MasterDataService.Interfaces;
using BloodBankCare.Services.SocialOrganizationInfoService.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BloodBankCare.Controllers
{
    public class DashBoardController : Controller
    {
        private readonly IUserInfoes userInfoes;
        private readonly IDonorInformationService _donorInformationService;
        private readonly IBloodRequestInfoService _bloodRequestInfoService;
        private readonly IDonationRecordInfoService _donationRecordInfoService;
        private readonly IAddressInfoService _addressInfoService;
        private readonly UserManager<ApplicationUser> _userManager;

        public readonly ISocialOrganizationService _socialOrganizationService;
        public readonly IProfessionService _professionService;
        public readonly IReligionService _religionService;
        public readonly ICountryService _countryService;
        public readonly IBloodGroupService _bloodGroupService;
        public readonly IGenderService _genderService;
        public readonly IDistrictService _districtService;
        public readonly IThanaService _thanaService;



        public DashBoardController(IBloodRequestInfoService bloodRequestInfoService,
            IDonationRecordInfoService donationRecordInfoService,
            IUserInfoes userInfoes,
            UserManager<ApplicationUser> userManager,
            IDonorInformationService donorInformationService,
            IAddressInfoService addressInfoService,
            ISocialOrganizationService socialOrganizationService,
            IProfessionService professionService,
            IReligionService religionService,
            ICountryService countryService,
            IBloodGroupService bloodGroupService,
            IGenderService genderService,
            IDistrictService districtService,
            IThanaService thanaService
         )
        {

            this.userInfoes = userInfoes;
            _donorInformationService = donorInformationService;
            _addressInfoService = addressInfoService;
            _userManager = userManager;
            _donationRecordInfoService = donationRecordInfoService;
            _bloodRequestInfoService = bloodRequestInfoService;
            _socialOrganizationService = socialOrganizationService;
            _professionService = professionService;
            _religionService = religionService;
            _countryService = countryService;
            _bloodGroupService = bloodGroupService;
            _genderService = genderService;
            _districtService = districtService;
            _thanaService = thanaService;
        }

        public IActionResult Index()
        {
            return View();
        }



        public async Task<IActionResult> UserDashboard()
        {
            ApplicationUser user = new ApplicationUser();
            if (User.Identity.Name == null || User.Identity.Name == "")
            {

                return Redirect("/Auth/Account/Login");

            }
            else
            {
                user = await userInfoes.GetUserInfoByUser(User.Identity.Name);
            }

        


            DonorInformation donor = new DonorInformation();
            donor = await _donorInformationService.GetDonorInformationByUserId(user.Id);
            var donation = await _donationRecordInfoService.GetAllDonationRecordInfo();


          UserDashBoardViewModel data = new UserDashBoardViewModel();
            if (donor!=null)
            {
                int? donationDayCount = 0;
                var donationRecordData = await _donationRecordInfoService.GetLastDonationRecordByUserId(user.Id);
                donationDayCount = Math.Abs(donationRecordData!=null?(DateTime.Now.Date-donationRecordData.needDate.Value.Date).Days:0);

                data = new UserDashBoardViewModel
                {
                    applicationUser = user,
                    BloodReceivedInfos = donation.Where(x => x.userId == user.Id).ToList(),
                    BloodRequestInfos = await _bloodRequestInfoService.GetBloodRequestInfoByUserId(user.Id),
                    LastDonationDayCount=donationDayCount,
                    donorInformation = await _donorInformationService.GetDonorInformationByUserId(user.Id),
                    presentAddress = await _addressInfoService.GetDonorPresentAddress(donor.Id),
                    permanentAddress = await _addressInfoService.GetDonorPermanentAddress(donor.Id),

                    donationRecord = await _donationRecordInfoService.GetLastDonationRecordByUserId(donor.userId),
                    TotalDonation = donation.Where(x => x.donorUserId == donor.userId).Sum(x => x.amountOfBlood),
                    DonationRecordInfos = await _donationRecordInfoService.GetDonationRecordByDonorUserId(user.Id),


                    //dropdown field value
                    religions = await _religionService.GetAllReligion(),
                    professions = await _professionService.GetAllProfession(),
                    socialOrganizations = await _socialOrganizationService.GetAllSocialOrganization(),
                    countries = await _countryService.GetAllCountry(),
                    districts = await _districtService.GetAllDistrict(),
                    thanas = await _thanaService.GetAllThana(),
                    bloodGroups = await _bloodGroupService.GetAllBloodGroup(),
                    genders = await _genderService.GetAllGender(),

                };
            }
            else
            {
                data = new UserDashBoardViewModel
                {
                    applicationUser = user,
                    BloodReceivedInfos = donation.Where(x => x.userId == user.Id).ToList(),
                    BloodRequestInfos = await _bloodRequestInfoService.GetBloodRequestInfoByUserId(user.Id),
                    LastDonationDayCount = 0,



                    //dropdown field value
                    religions = await _religionService.GetAllReligion(),
                    professions = await _professionService.GetAllProfession(),
                    socialOrganizations = await _socialOrganizationService.GetAllSocialOrganization(),
                    countries = await _countryService.GetAllCountry(),
                    districts = await _districtService.GetAllDistrict(),
                    thanas = await _thanaService.GetAllThana(),
                    bloodGroups = await _bloodGroupService.GetAllBloodGroup(),
                    genders = await _genderService.GetAllGender(),

                };
            }

         


            if (data.donorInformation==null)
            {
                data.donorInformation = new DonorInformation();
            }
            if (data.presentAddress==null)
            {
                data.presentAddress = new AddressInfo();
            }
            if (data.permanentAddress == null)
            {
                data.permanentAddress = new AddressInfo();
            } 
            if (data.DonationRecordInfos == null)
            {
                
                data.DonationRecordInfos = new List<DonationRecordInfo>();
            }
            if (data.BloodReceivedInfos == null)
            {
                data.BloodReceivedInfos = new List<DonationRecordInfo>();
            }
            if (data.BloodRequestInfos == null)
            {
                data.BloodRequestInfos = new List<BloodRequestInfo>();
            }
           

            return View(data);




        }


        public IActionResult SuperAdminDashBoard()
        {
            return View();
        }






    }
}
