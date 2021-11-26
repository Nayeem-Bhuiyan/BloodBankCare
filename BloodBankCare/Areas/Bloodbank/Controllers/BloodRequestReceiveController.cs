using BloodBankCare.Areas.Bloodbank.Models;
using BloodBankCare.Data.Entity.ApplicationUsers;
using BloodBankCare.Data.Entity.Bloodbank;
using BloodBankCare.Services.AuthService.Interfaces;
using BloodBankCare.Services.BloodbankService.Interfaces;
using BloodBankCare.Services.MasterDataService.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Areas.Bloodbank.Controllers
{

    [Area("Bloodbank")]
    public class BloodRequestReceiveController : Controller
    {
        public readonly IBloodRequestReceivedInfoService BloodRequestReceivedInfoService;
        public readonly IDonationRecordInfoService donationRecordInfoService;
        private readonly UserManager<ApplicationUser> _userManager;
        public readonly IBloodRequestInfoService BloodRequestInfoService;
        public readonly IBloodGroupService BloodGroupService;
        public readonly IRelationShipService relationShipService;
        private readonly IUserInfoes userInfoes;
        public BloodRequestReceiveController(IBloodRequestReceivedInfoService _BloodRequestReceivedInfoService,
            UserManager<ApplicationUser> userManager,
            IBloodRequestInfoService _BloodRequestInfoService,
            IBloodGroupService _BloodGroupService,
            IRelationShipService _relationShipService,
            IUserInfoes _userInfoes,
            IDonationRecordInfoService _donationRecordInfoService
        )
        {
            BloodRequestReceivedInfoService = _BloodRequestReceivedInfoService;
            _userManager = userManager;
            BloodRequestInfoService = _BloodRequestInfoService;
            BloodGroupService = _BloodGroupService;
            relationShipService = _relationShipService;
            userInfoes = _userInfoes;
            donationRecordInfoService = _donationRecordInfoService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {

            try
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
                    List<BloodRequestReceivedInfo> receivedDataList=new List<BloodRequestReceivedInfo>();
                   
                    var data = await BloodRequestReceivedInfoService.GetAllBloodRequestReceivedInfo();
                    var dataList = await BloodRequestReceivedInfoService.GetUserBloodRequestReceivedList();
                    foreach (var item in dataList)
                    {
                        var checkAsseptData = await BloodRequestReceivedInfoService.GetBloodRequestReceivedInfoByUserId(user.Id, item.BloodRequestInfoId);
                        if (checkAsseptData == null)
                        {
                            receivedDataList.Add(item);
                        }
                    }

                    BloodRequestReceivedInfoViewModel model = new BloodRequestReceivedInfoViewModel
                    {
                        bloodRequestReceivedInfos = receivedDataList.Where(x=>x.BloodRequestInfo.userId != user.Id),
                        ApplicationUsers = await userInfoes.GetUsers(),
                        CurrentUser=user
                    };
                    return View(model);
                }
             }
            catch (Exception)
            {

                throw;
            }


        }







        #region Api

        //Blood request Accept by donor

        [HttpPost]
        public async Task<IActionResult> Index(int Id,int? BloodRequestInfoId)  //id=bloodRequestReceivedId
        {

            try
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

                if (user.Id==null ||user.Id=="")
                {
                    return Redirect("/Auth/Account/Login");
                }
                else
                {
                  var dataObj = await BloodRequestReceivedInfoService.GetBloodRequestReceivedInfoByUserId(user.Id, BloodRequestInfoId);

                 
                    BloodRequestReceivedInfo data = new BloodRequestReceivedInfo
                        {
                            Id = dataObj==null?0: dataObj.Id,
                            BloodRequestInfoId = BloodRequestInfoId, //accept button theke asbe not dropdown
                            isAccept = 1,  //0=pending(user) ,1=accept(donor)
                            acceptedBy = user.Id,  //donorUserId: accept button e click korle ei user Id Porbe
                            isDonated = 1, //0=pending,1=onProcess,2=donated
                            remarks = "DonorAccepted",
                            
                        };
                        await BloodRequestReceivedInfoService.SaveBloodRequestReceivedInfo(data);
                        return RedirectToAction(nameof(Index));

                }
                
            }
            catch (Exception)
            {
                throw;
            }

        }



        [HttpGet]
        public async Task<IActionResult> RequestAcceptedList()  //request accepted List By Donor
        {

            try
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
                  

                    var data = await BloodRequestReceivedInfoService.GetDonorAcceptedList(user.Id);
                 

                    BloodRequestReceivedInfoViewModel model = new BloodRequestReceivedInfoViewModel
                    {
                        bloodRequestReceivedInfos = data.Where(x => x.BloodRequestInfo.userId != user.Id),
                        ApplicationUsers = await userInfoes.GetUsers(),
                        CurrentUser = user
                    };
                    return View(model);
                }
            }
            catch (Exception)
            {

                throw;
            }


        }


        [HttpGet]
        public async Task<IActionResult> DonateApproval()
        {

            try
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
                    List<BloodRequestReceivedInfo> receivedDataList = new List<BloodRequestReceivedInfo>();

                    var data = await BloodRequestReceivedInfoService.GetAllBloodRequestReceivedInfo();

                    BloodRequestReceivedInfoViewModel model = new BloodRequestReceivedInfoViewModel
                    {
                        bloodRequestReceivedInfos = data.Where(x => x.BloodRequestInfo.userId != user.Id),
                        ApplicationUsers = await userInfoes.GetUsers(),
                        CurrentUser = user
                    };
                    return View(model);
                }
            }
            catch (Exception)
            {

                throw;
            }


        }







        [HttpPost]
        public async Task<IActionResult> DonatedConfirm(int Id, int? BloodRequestInfoId)  //id=BloodRequestReceivedId
        {

            try
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
                if (user.Id == null || user.Id=="")
                {
                    return Redirect("/Auth/Account/Login");
                }
                else
                {
                    var dataObj = await BloodRequestReceivedInfoService.GetBloodRequestReceivedInfoByUserId(user.Id, BloodRequestInfoId);
                
                        BloodRequestReceivedInfo data = new BloodRequestReceivedInfo
                        {
                            Id = dataObj.Id > 0 ? dataObj.Id :Id,
                            BloodRequestInfoId = BloodRequestInfoId, //accept button theke asbe not dropdown
                            isAccept = 1,  //0=pending(user) ,1=accept(donor)
                            acceptedBy = user.Id,  //donorUserId: accept button e click korle ei user Id Porbe
                            isDonated = 2, //0=pending,1=onProcess,2=donated
                            remarks="Donated"
                        };
                        await BloodRequestReceivedInfoService.SaveBloodRequestReceivedInfo(data);
                        return RedirectToAction(nameof(Index));
                }

            }
            catch (Exception)
            {
                throw;
            }

        }




        [HttpPost]
        public async Task<IActionResult> RequestCancel(int Id, int? BloodRequestInfoId)
        {

            try
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

                    await BloodRequestReceivedInfoService.DeleteBloodRequestReceivedInfoById(Id);
                    return RedirectToAction(nameof(Index));
                }

            }
            catch (Exception)
            {
                throw;
            }

        }














        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            bool response = false;
            try
            {
                response = await BloodRequestReceivedInfoService.DeleteBloodRequestReceivedInfoById(id);
            }
            catch (Exception ex)
            {
                throw ex;

            }

            return Json(response);
        }
        #endregion



    }
}
