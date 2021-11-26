using BloodBankCare.Data.Entity.ApplicationUsers;
using BloodBankCare.Data.Entity.Bloodbank;
using BloodBankCare.Data.Entity.Home;
using BloodBankCare.Data.Entity.MasterData;
using BloodBankCare.Data.Entity.NoticeBoard;
using BloodBankCare.Models;
using BloodBankCare.Services.AdminPanelServices.Interfaces;
using BloodBankCare.Services.AuthService.Interfaces;
using BloodBankCare.Services.BloodbankService.Interfaces;
using BloodBankCare.Services.HomeService.Interfaces;
using BloodBankCare.Services.MasterDataService.Interfaces;
using BloodBankCare.Services.NoticeBoardService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Controllers
{
    public class HomeController : Controller
    {


        private readonly IBloodCampaignService _bloodCampaignService;
        private readonly IContactUsService _contactUsService;
        private readonly IDailyNewsUpdateService _dailyNewsUpdateService;
        private readonly IGeneralFAQsService _generalFAQsService;
        private readonly IHospitalDetailsService _hospitalDetailsService;
        private readonly IOtherBloodBankDetailsService _otherBloodBankDetailsService;
        private readonly IPhotoGalleryService _photoGalleryService;
        private readonly IWellWisherMessageService _wellWisherMessageService;
        private readonly IAdminPanelService _adminPanelService;
        private readonly INoticeBoardInfoService _noticeBoardInfoService;
        private readonly IDonationRecordInfoService _donationRecordInfoService;
        private readonly IUserInfoes _userInfoes;
        private readonly IDonorInformationService _donorInformationService;
        private readonly IBloodGroupService _bloodGroupService;
    

        public HomeController(
            
         IBloodCampaignService bloodCampaignService,
         IContactUsService contactUsService,
         IDailyNewsUpdateService dailyNewsUpdateService,
         IGeneralFAQsService generalFAQsService,
         IHospitalDetailsService hospitalDetailsService,
         IOtherBloodBankDetailsService otherBloodBankDetailsService,
            IPhotoGalleryService photoGalleryService,
            IWellWisherMessageService wellWisherMessageService,
            IAdminPanelService adminPanelService,
            INoticeBoardInfoService noticeBoardInfoService,
            IDonationRecordInfoService donationRecordInfoService,
            IUserInfoes userInfoes,
            IDonorInformationService donorInformationService,
            IBloodGroupService bloodGroupService
        )
        {
            _photoGalleryService = photoGalleryService;
            _otherBloodBankDetailsService = otherBloodBankDetailsService;
            _hospitalDetailsService = hospitalDetailsService;
            _bloodCampaignService = bloodCampaignService;
            _contactUsService = contactUsService;
            _dailyNewsUpdateService = dailyNewsUpdateService;
            _generalFAQsService = generalFAQsService;
            _wellWisherMessageService = wellWisherMessageService;
            _adminPanelService = adminPanelService;
            _noticeBoardInfoService = noticeBoardInfoService;
            _donationRecordInfoService = donationRecordInfoService;
            _userInfoes = userInfoes;
            _donorInformationService = donorInformationService;
            _bloodGroupService = bloodGroupService;
        }

        public async Task<IActionResult> Index()
        {


            var donations = await _donationRecordInfoService.GetAllDonationRecordInfo();

            HomePageViewModel data = new HomePageViewModel
            {
                dailyNewsUpdates = await _dailyNewsUpdateService.GetAllDailyNewsUpdate(),
                noticeBoardInfos = await _noticeBoardInfoService.GetAllNoticeBoardInfo(),
                bloodGroupWiseDonorViewModels = await BloodGroupWiseDonors(),

                topDonorViewModels = await ListOfTopDonors(),
                bloodCampaigns = await _bloodCampaignService.GetAllBloodCampaign(),
                generalFAQs = await _generalFAQsService.GetAllGeneralFAQS(),
                hospitalDetails = await _hospitalDetailsService.GetAllHospitalDetails(),
                otherBloodBankDetails = await _otherBloodBankDetailsService.GetAllOtherBloodBankDetails(),
                photoGalleries = await _photoGalleryService.GetAllPhotoGallery(),
                wellWisherMessages = await _wellWisherMessageService.GetAllWellWisherMessage(),
                adminPanelInfos = await _adminPanelService.GetAllAdminPanelInfo(),
                
                applicationUsers = await _userInfoes.GetUsers(),
                donors = await _donorInformationService.GetAllDonorInformation(),
                countRecordInfos = await CountRecordSummary(),
                lastDonationRecord = donations.OrderByDescending(x=>x.needDate).FirstOrDefault()
            };

            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> DailyNewsUpdates(int? id)
        {

            IEnumerable<DailyNewsUpdate> data = await _dailyNewsUpdateService.GetAllDailyNewsUpdate();
            return View(data);
        }

        public async Task<IActionResult> NoticeDetails(int? id)
        {
           
            NoticeBoardInfo data = await _noticeBoardInfoService.GetNoticeBoardInfoById(id);
            return View(data);
         }


            [HttpGet]
        public IActionResult ContactUS()
        {
            ContactUSViewModel model = new ContactUSViewModel()
            {

            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ContactUS([FromForm] ContactUSViewModel model)
        {
            try
            {

                ContactUS data = new ContactUS
                {
                    name = model.name,
                    email = model.email,
                    contactNo = model.contactNo,
                    subject = model.subject,
                    message = model.message
                };
                await _contactUsService.SaveContactUS(data);
                return RedirectToAction(nameof(ContactUS));
            }
            catch (Exception)
            {

                throw;
            }


            
        }


        public async Task<IActionResult> PhotoGallery()
        {
            HomePageViewModel data = new HomePageViewModel
            {
                photoGalleries = await _photoGalleryService.GetAllPhotoGallery()
            };
            return View(data);
        }


        public async Task<IActionResult> ImportanHospitalDetails()
        {
            HomePageViewModel data = new HomePageViewModel
            {
                hospitalDetails = await _hospitalDetailsService.GetAllHospitalDetails()
            };
            return View(data);
        }



        public async Task<IActionResult> OtherBloodBankInfo()
        {
            HomePageViewModel data = new HomePageViewModel
            {
                otherBloodBankDetails = await _otherBloodBankDetailsService.GetAllOtherBloodBankDetails()
            };
            return View(data);
        }

        public async Task<IActionResult> AdminPanelInfo()
        {
            HomePageViewModel data = new HomePageViewModel
            {
                adminPanelInfos = await _adminPanelService.GetAllAdminPanelInfo()
            };
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> DonorStatusInfo()
        {
            List<DonorStatusViewModel> ListModel = new List<DonorStatusViewModel>();

            List<DonorInformation> AllDonatedDonors = new List<DonorInformation>();
            List<DonationRecordInfo> donationRecordInfoOfDonor = new List<DonationRecordInfo>();

            var donors = await _donorInformationService.GetAllDonorInformation();
            var donations= await _donationRecordInfoService.GetAllDonationRecordInfo();
           var distinctDonorUserIdList= donations.Select(x => x.donorUserId).Distinct().ToList();

            foreach (var donorUserId in distinctDonorUserIdList)
            {
                AllDonatedDonors.Add(await _donorInformationService.GetDonorInformationByUserId(donorUserId));

     
                donationRecordInfoOfDonor.AddRange(await _donationRecordInfoService.GetDonationRecordByDonorUserId(donorUserId));
            }

            foreach (var donation in donationRecordInfoOfDonor.Distinct())
            {
                AllDonatedDonors.Add(await _donorInformationService.GetDonorInformationByUserId(donation.donorUserId));
                int dateDiff= Math.Abs((donation.needDate.Value.Date - DateTime.Now.Date).Days);

                int? donationCount = 0;
                var  listDonation=await _donationRecordInfoService.GetDonationRecordByDonorUserId(donation.donorUserId);
                foreach (var dinationByUser in listDonation)
                {
                    donationCount= donationCount+dinationByUser.amountOfBlood;
                };
               
                if (dateDiff<=120)
                {
                    DonorStatusViewModel NotReadyDonatedDonor = new DonorStatusViewModel()
                    {
                        donorInformation = await _donorInformationService.GetDonorInformationByUserId(donation.donorUserId),
                        lastDonationDaysCount = dateDiff,
                        LastDonatedDate= donation.needDate.Value.Date,
                        donationStatus = "Dontated",
                        donorType = "Experienced Donor",
                        donorStatus="Not Ready For Donate",
                        OrderByRecord = 3,
                        totalDonation= donationCount,
                        donorUserId=donation.donorUserId
                    };
                 
                        ListModel.Add(NotReadyDonatedDonor);
                    
                       
                    
                   
                }
                else
                {
                    DonorStatusViewModel ReadyDonatedDonor = new DonorStatusViewModel()
                    {
                        donorInformation = await _donorInformationService.GetDonorInformationByUserId(donation.donorUserId),
                        lastDonationDaysCount = dateDiff,
                        LastDonatedDate = donation.needDate.Value.Date,
                        donationStatus = "Dontated",
                        donorType = "Experienced Donor",
                        donorStatus = "Ready For Donate",
                        OrderByRecord = 1,
                        totalDonation=donationCount
                    };
                    
                   ListModel.Add(ReadyDonatedDonor);

                  
                }
            }

           var freshdonors= donors.Except(AllDonatedDonors);
            foreach (var freshdonor in freshdonors)
            {
                DonorStatusViewModel FreshDonor = new DonorStatusViewModel()
                {
                    donorInformation = freshdonor,
                    lastDonationDaysCount =0,
                    LastDonatedDate =null,
                    donationStatus = "Fresher",
                    donorType = "Fresh Donor",
                    donorStatus = "Ready For Donate",
                    OrderByRecord = 2,
                    totalDonation=0
                };
                ListModel.Add(FreshDonor);
            }


           


            return View(ListModel);
        }


        public async Task<CountRecordViewModel> CountRecordSummary()
        {

            CountRecordViewModel model = new CountRecordViewModel();

            var donations = await _donationRecordInfoService.GetAllDonationRecordInfo();
            int TotalDonatedDonors = 0;
            TotalDonatedDonors = donations.Select(x => x.donorUserId).Distinct().Count();
            int? TotalDonationOfCurrentMonth = donations.Where(x => x.needDate.Value.Month == DateTime.Now.Month && x.needDate.Value.Year==DateTime.Now.Year).ToList().Sum(x=>x.amountOfBlood);
            int? totalDonationAmount = 0;
            foreach (var item in donations)
            {
                totalDonationAmount = totalDonationAmount + item.amountOfBlood;


            }

            var users = await _userInfoes.GetUsers();
            var donors = await _donorInformationService.GetAllDonorInformation();
            model = new CountRecordViewModel
            {
                totalDonatedDonors= TotalDonatedDonors,
                totalDonationOfCurrentMonth= TotalDonationOfCurrentMonth,
                totalDonation= totalDonationAmount,
                totalUsers = users.Select(x=>x.Id).Distinct().ToList().Count(),
                totalDonors= donors.Select(x=>x.userId).Distinct().ToList().Count()
            };

            return model;
        }

        public async Task<IEnumerable<BloodGroupWiseDonorViewModel>> BloodGroupWiseDonors()
        {
            List<BloodGroupWiseDonorViewModel> bloodGroupListInfo = new List<BloodGroupWiseDonorViewModel>();
            var users = await _userInfoes.GetUsers();

            int BloodGroupWiseDonor = 0;
            BloodGroup bGroupInfo = new BloodGroup();
            var AllBloodGroups = await _bloodGroupService.GetAllBloodGroup();
            foreach (var item in AllBloodGroups)
            {
                bGroupInfo = await _bloodGroupService.GetBloodGroupById(item.Id);
                
                BloodGroupWiseDonor = users.Where(x => x.BloodGroupId == item.Id).ToList().Count();
                BloodGroupWiseDonorViewModel group = new BloodGroupWiseDonorViewModel()
                {
                    bloodGroup = bGroupInfo,
                    TotalDonor = BloodGroupWiseDonor,
                    TotalDonation= await _donationRecordInfoService.TotalDonationByBloodGroup(item.Id),
                    bloodGroupId=item.Id
                };
                bloodGroupListInfo.Add(group);
            }
            return bloodGroupListInfo;
        }

        public async Task<IEnumerable<TopDonorViewModel>> ListOfTopDonors()
        {
            List<TopDonorViewModel> topDonorList = new List<TopDonorViewModel>();
            List<string> UserIdList = new List<string>();
            var donations =await _donationRecordInfoService.GetAllDonationRecordInfo();

            foreach (var donation in donations)
            {
                UserIdList.Add(donation.donorUserId);

            }

            string[] donarUserIdList = UserIdList.ToArray();
            int? donationSum = 0;

            List<DonationRecordInfo> donorDonationList = new List<DonationRecordInfo>();

            foreach (var donorUserId in donarUserIdList.Distinct())
            {
                donorDonationList = donations.Where(x => x.donorUserId == donorUserId).ToList();

                foreach (var donationRecord in donorDonationList)
                {
                    donationSum = donationSum + donationRecord.amountOfBlood;
                }

                TopDonorViewModel topDonor = new TopDonorViewModel();
                topDonor.totalDonationAmount = donationSum != 0 ? donationSum : 0;
                topDonor.userId = donorUserId;

                topDonorList.Add(topDonor);
            }

            topDonorList.OrderByDescending(x => x.totalDonationAmount).ToList();

            return topDonorList;
        }


        public async Task<IActionResult> AdminDashBoard()
        {
            var donations = await _donationRecordInfoService.GetAllDonationRecordInfo();

            
            
            AdminDashBoardViewModel vm = new AdminDashBoardViewModel
            {
                countRecordInfos = await CountRecordSummary(),
                groupWiseDonors=await BloodGroupWiseDonors(),
                CurrentMonthDonations= donations.Where(x => x.needDate.Value.Month == DateTime.Now.Month && x.needDate.Value.Year == DateTime.Now.Year).ToList(),
                LastMonthDonations= donations.Where(x => x.needDate.Value.Month == DateTime.Now.Month-1 && x.needDate.Value.Year == DateTime.Now.Year).ToList(),
            };


            return View(vm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
