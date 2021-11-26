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
    public class SocialOrganizationBrunchsController : Controller
    {
        public readonly ISocialOrganizationBrunchService SocialOrganizationBrunchService;
        public readonly ISocialOrganizationService socialOrganizationService;

        public SocialOrganizationBrunchsController(ISocialOrganizationBrunchService _SocialOrganizationBrunchService, ISocialOrganizationService _socialOrganizationService)
        {
            SocialOrganizationBrunchService = _SocialOrganizationBrunchService;
            socialOrganizationService = _socialOrganizationService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            SocialOrganizationBrunchViewModel model = new SocialOrganizationBrunchViewModel
            {
                SocialOrganizationBrunchs = await SocialOrganizationBrunchService.GetAllSocialOrganizationBrunch(),
                socialOrganizations = await socialOrganizationService.GetAllSocialOrganization(),
            };
            return View(model);
        }


        #region Api

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] SocialOrganizationBrunchViewModel model)
        {

            try
            {
                SocialOrganizationBrunch data = new SocialOrganizationBrunch
                {
                    Id = model.SocialOrganizationBrunchId,
                     brunchName =model.brunchName,
                     brunchLocation =model.brunchLocation,
                     SocialOrganizationId =model.SocialOrganizationId,  //fk
                };

                await SocialOrganizationBrunchService.SaveSocialOrganizationBrunch(data);
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
                response = await SocialOrganizationBrunchService.DeleteSocialOrganizationBrunchById(id);
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
