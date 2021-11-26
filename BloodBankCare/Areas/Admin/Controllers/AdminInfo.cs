using BloodBankCare.Areas.Admin.Models;
using BloodBankCare.Data.Entity.Admin;
using BloodBankCare.Data.Entity.ApplicationUsers;
using BloodBankCare.Helpers;
using BloodBankCare.Services.AdminPanelServices.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminInfo : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public readonly IAdminPanelService adminPanelService;

        public AdminInfo(IAdminPanelService _adminPanelService, UserManager<ApplicationUser> userManager)
        {
            adminPanelService = _adminPanelService;
            _userManager = userManager;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            AdminPanelInfoViewModel model = new AdminPanelInfoViewModel
            {
                adminPanelInfos = await adminPanelService.GetAllAdminPanelInfo(),
            };
            return View(model);
        }


        #region Api

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] AdminPanelInfoViewModel model)
        {

            try
            {

                string imageUrl = "DefaultImage/NoImage.jpg";

                string fileName;
                string message = FileSave.SaveImage(out fileName, model.UploadImage);
                if (message == "success")
                {
                    imageUrl = "";
                    imageUrl = fileName;
                }
                else
                {
                    return View(model);
                }



                ApplicationUser user = await _userManager.FindByNameAsync(User.Identity.Name);

                if (user.Id!=null)
                {
                    AdminPanelInfo data = new AdminPanelInfo
                    {
                        Id = model.AdminPanelInfoId,
                        name = model.name,
                        contactNo = model.contactNo,
                        userId = user.Id,
                        profession = model.profession,
                        instituteName = model.instituteName,
                        instituteType = model.instituteType,  //business,govt,pvt,multinational,other
                        designation = model.designation,
                        address = model.address,
                        role=model.role,
                        ImageUrl= imageUrl
                        
                    };
                    await adminPanelService.SaveAdminPanelInfo(data);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return Redirect("/Auth/Account/Login");
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
                response = await adminPanelService.DeleteAdminPanelInfoById(id);
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
