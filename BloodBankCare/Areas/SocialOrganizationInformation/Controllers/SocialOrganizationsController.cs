using BloodBankCare.Areas.SocialOrganizationInformation.Models;
using BloodBankCare.Data.Entity.SocialOrganizationInfo;
using BloodBankCare.Services.SocialOrganizationInfoService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Areas.SocialOrganizationInformation.Controllers
{
   [Area("SocialOrganizationInformation")]
    public class SocialOrganizationsController : Controller
    {
        public readonly ISocialOrganizationService SocialOrganizationService;

        public SocialOrganizationsController(ISocialOrganizationService _SocialOrganizationService)
        {
            SocialOrganizationService = _SocialOrganizationService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            SocialOrganizationViewModel model = new SocialOrganizationViewModel
            {
                socialOrganizations = await SocialOrganizationService.GetAllSocialOrganization(),
            };
            return View(model);
        }


        #region Api

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] SocialOrganizationViewModel model)
        {

            try
            {
                SocialOrganization data = new SocialOrganization
                {
                    Id = model.SocialOrganizationId,
                    socialOrgName=model.socialOrgName
                };

                await SocialOrganizationService.SaveSocialOrganization(data);
                return RedirectToAction(nameof(Index));
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
                response = await SocialOrganizationService.DeleteSocialOrganizationById(id);
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
