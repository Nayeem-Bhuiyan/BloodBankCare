using BloodBankCare.Areas.Bloodbank.Models;
using BloodBankCare.Data.Entity.ApplicationUsers;
using BloodBankCare.Data.Entity.Bloodbank;
using BloodBankCare.Services.AuthService.Interfaces;
using BloodBankCare.Services.BloodbankService.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Areas.Bloodbank.Controllers
{
    [Area("Bloodbank")]
    public class DonationRecordController : Controller
    {
        public readonly IBloodRequestReceivedInfoService BloodRequestReceivedInfoService;
        public readonly IDonorInformationService _donorInformationService;
        public readonly IDonationRecordInfoService donationRecordInfoService;
        private readonly IUserInfoes _userInfoes;
        private readonly UserManager<ApplicationUser> _userManager;

        public DonationRecordController(IBloodRequestReceivedInfoService _BloodRequestReceivedInfoService,
            IDonationRecordInfoService _donationRecordInfoService,
            UserManager<ApplicationUser> userManager,
            IDonorInformationService donorInformationService,
            IUserInfoes userInfoes
        )
        {
            BloodRequestReceivedInfoService = _BloodRequestReceivedInfoService;
            donationRecordInfoService = _donationRecordInfoService;
            _userManager = userManager;
            _donorInformationService = donorInformationService;
            _userInfoes = userInfoes;
        }

        public async Task<IActionResult> Index()
        {

            DonationRecordInfoViewModel model = new DonationRecordInfoViewModel()
            {
                donationRecordInfos = await donationRecordInfoService.GetAllDonationRecordInfo(),
                ApplicationUsers = await _userInfoes.GetUsers()
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Index(DonationRecordInfoViewModel model)
        {
            ApplicationUser user = new ApplicationUser();

            if (User.Identity.Name == null || User.Identity.Name == "")
            {

                return RedirectToAction("Login", "Account", new { area = "Auth" });
            }
            else
            {
                user = await _userManager.FindByNameAsync(User.Identity.Name);
            }
            if (user.Id == null || user.Id == "")
            {
                return Redirect("/Auth/Account/Login");
            }
            else
            {



                DonationRecordInfo data = new DonationRecordInfo
                {
                    Id = model.DonationRecordInfoId,
                    userId = model?.userId,      //userId==request UserId
                    //donorUserId = model.acceptedBy,    //donorUserId=accepted by
                    requestDate = model.requestDate,
                    needDate = model?.needDate,
                    donationPlaceName = model?.donationPlaceName,
                    location = model?.location,
                    amountOfBlood = model?.amountOfBlood,
                    patientName = model?.patientName,
                    patientAddress = model?.patientAddress,
                    reasonOfBlood = model?.reasonOfBlood,
                    BloodGroupId = model.BloodGroupId,
                    isApproved = 1,  //0=pending(request),1=approved(Admin),2=Rejected(admin)
                    approver = user.Id  //approver user id or name(super Admin)
                };
                await donationRecordInfoService.SaveDonationRecordInfo(data);
                return Redirect("/Bloodbank/DonationRecord/DonationRecordApproval");
            }
        }

        public async Task<IActionResult> DonationRecordApproval()
        {
            BloodRequestReceivedInfoViewModel model = new BloodRequestReceivedInfoViewModel
            {
                bloodRequestReceivedInfos = await BloodRequestReceivedInfoService.GetAllBloodRequestReceivedInfo(),
                ApplicationUsers = await _userInfoes.GetUsers()
            };
            return View(model);
        }

        public async Task<IActionResult> CreateDonationWithApproval(int? bloodRequestInfoId, int? bloodRequestReciveId)
        {
            var model = await BloodRequestReceivedInfoService.GetBloodRequestReceivedInfoByReceivedId(bloodRequestReciveId, bloodRequestInfoId);

            model.remarks="DonationComplete";
            await BloodRequestReceivedInfoService.SaveBloodRequestReceivedInfo(model);

            ApplicationUser user = new ApplicationUser();

            if (User.Identity.Name == null || User.Identity.Name == "")
            {

                return RedirectToAction("Login", "Account", new { area = "Auth" });
            }
            else
            {
                user = await _userManager.FindByNameAsync(User.Identity.Name);
            }
            if (user.Id == null || user.Id == "")
            {
                return Redirect("/Auth/Account/Login");
            }
            else
            {

                var donation = await donationRecordInfoService.GetDonationRecordByUserAndRequestId(model.acceptedBy, model.BloodRequestInfo.userId, bloodRequestInfoId);

                DonationRecordInfo data = new DonationRecordInfo
                {
                    Id = donation != null ? donation.Id : 0,
                    userId = model.BloodRequestInfo?.userId,      //userId==request UserId
                    donorUserId = model.acceptedBy,    //donorUserId=accepted by
                    BloodRequestInfoId=bloodRequestInfoId,
                    requestDate = model.BloodRequestInfo?.requestDate,
                    needDate = model.BloodRequestInfo?.needDate,
                    donationPlaceName = model.BloodRequestInfo?.donationPlaceName,
                    location = model.BloodRequestInfo?.location,
                    amountOfBlood = model.BloodRequestInfo?.amountOfBlood,
                    patientName = model.BloodRequestInfo?.patientName,
                    patientAddress = model.BloodRequestInfo?.patientAddress,
                    reasonOfBlood = model.BloodRequestInfo?.reasonOfBlood,
                    BloodGroupId = model.BloodRequestInfo.BloodGroupId,
                    isApproved = 1,  //0=pending(request),1=approved(Admin),2=Rejected(admin)
                    approver = user.Id  //approver user id or name(super Admin)
                };
                await donationRecordInfoService.SaveDonationRecordInfo(data);
                return Redirect("/Bloodbank/DonationRecord/DonationRecordApproval");
            }


        }



        public async Task<IActionResult> DonationReject(int? bloodRequestInfoId, int? bloodRequestReciveId)
        {
            var model = await BloodRequestReceivedInfoService.GetBloodRequestReceivedInfoByReceivedId(bloodRequestReciveId, bloodRequestInfoId);

            ApplicationUser user = new ApplicationUser();

            if (User.Identity.Name == null || User.Identity.Name == "")
            {

                return RedirectToAction("Login", "Account", new { area = "Auth" });
            }
            else
            {
                user = await _userManager.FindByNameAsync(User.Identity.Name);
            }
            if (user.Id == null || user.Id == "")
            {
                return Redirect("/Auth/Account/Login");
            }
            else
            {

                DonationRecordInfo data = new DonationRecordInfo
                {
                    userId = model.BloodRequestInfo?.userId,      //userId==request UserId
                    donorUserId = model.acceptedBy,    //donorUserId=accepted by
                    requestDate = model.BloodRequestInfo?.requestDate,
                    needDate = model.BloodRequestInfo?.needDate,
                    donationPlaceName = model.BloodRequestInfo?.donationPlaceName,
                    location = model.BloodRequestInfo?.location,
                    amountOfBlood = model.BloodRequestInfo?.amountOfBlood,
                    patientName = model.BloodRequestInfo?.patientName,
                    patientAddress = model.BloodRequestInfo?.patientAddress,
                    reasonOfBlood = model.BloodRequestInfo?.reasonOfBlood,
                    BloodGroupId = model.BloodRequestInfo.BloodGroupId,
                    isApproved = 2,  //0=pending(request),1=approved(Admin),2=Rejected(admin)
                    approver = user.Id  //approver user id or name(super Admin)
                };



                await donationRecordInfoService.SaveDonationRecordInfo(data);
                return Redirect("/Bloodbank/DonationRecord/DonationRecordApproval");
            }


        }
    }
}
