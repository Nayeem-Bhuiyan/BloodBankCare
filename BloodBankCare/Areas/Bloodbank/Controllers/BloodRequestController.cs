using BloodBankCare.Areas.Bloodbank.Models;
using BloodBankCare.Data.Entity.ApplicationUsers;
using BloodBankCare.Data.Entity.Bloodbank;
using BloodBankCare.Services.BloodbankService.Interfaces;
using BloodBankCare.Services.MasterDataService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel;
using Microsoft.AspNetCore.Identity;

namespace BloodBankCare.Areas.Bloodbank.Controllers
{
   [Area("Bloodbank")]
    
    public class BloodRequestController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public readonly IBloodRequestInfoService BloodRequestInfoService;
        public readonly IBloodGroupService BloodGroupService;
        public readonly IRelationShipService relationShipService;
        public readonly IBloodRequestReceivedInfoService BloodRequestReceivedInfoService;
        public BloodRequestController(IBloodRequestInfoService _BloodRequestInfoService, 
            IBloodGroupService _BloodGroupService, IRelationShipService _relationShipService, 
            UserManager<ApplicationUser> userManager,
            IBloodRequestReceivedInfoService _BloodRequestReceivedInfoService
            )
        {
            BloodRequestInfoService = _BloodRequestInfoService;
            BloodGroupService = _BloodGroupService;
            relationShipService = _relationShipService;
            _userManager = userManager;
            BloodRequestReceivedInfoService = _BloodRequestReceivedInfoService;
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
                    BloodRequestInfoViewModel model = new BloodRequestInfoViewModel
                    {
                        bloodRequestInfos = await BloodRequestInfoService.GetAllBloodRequestInfo(),
                        bloodGroups = await BloodGroupService.GetAllBloodGroup(),
                        relationShips = await relationShipService.GetAllRelationShip(),
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] BloodRequestInfoViewModel model)
        {

            int ans = 0;
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

                    BloodRequestInfo data = new BloodRequestInfo
                    {
                        Id = model.BloodRequestInfoId,
                        userId = user.Id,
                        patientName = model.patientName,
                        patientAddress = model.patientAddress,
                        reasonOfBlood = model.reasonOfBlood,
                        BloodGroupId = model.BloodGroupId,
                        amountOfBlood = model.amountOfBlood,
                        donationPlaceName = model.donationPlaceName,
                        location = model.location,
                        needDate =model.needDate,
                        RelationShipId = model.RelationShipId,
                        requestDate = DateTime.Now,
                        remarks=model.remarks,
                       
                    };


                    ans= await BloodRequestInfoService.SaveBloodRequestInfo(data);


                    if (ans>0)
                    {
                        if (data.Id>0)
                        {

                            BloodRequestReceivedInfo checkDataInfo = await BloodRequestReceivedInfoService.GetBloodRequestReceivedInfoOfUserRequest(data.Id);
                            if (checkDataInfo==null)
                            {
                                BloodRequestReceivedInfo dataList = new BloodRequestReceivedInfo
                                {
                                    Id = 0,
                                    BloodRequestInfoId = data.Id, //accept button theke asbe not dropdown
                                    isAccept = 0,  //0=pending(user) ,1=accept(donor)
                                    isDonated = 0, //0=No,
                                    remarks = "UserRequest"
                                };

                                await BloodRequestReceivedInfoService.SaveBloodRequestReceivedInfo(dataList);
                            }
                           
                        }
                        else
                        {
                            return View(model);
                        }
                    }
                    else
                    {
                        return View(model);
                    }
                   



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
             var data=await BloodRequestReceivedInfoService.GetBloodRequestReceivedInfoByRequestId(id);
                var withOutDonationList=data.Where(x => x.isDonated != 2).ToList();
                await BloodRequestReceivedInfoService.DeleteMultipleBloodRequestReceivedInfo(withOutDonationList);
                response = await BloodRequestInfoService.DeleteBloodRequestInfoById(id);
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
