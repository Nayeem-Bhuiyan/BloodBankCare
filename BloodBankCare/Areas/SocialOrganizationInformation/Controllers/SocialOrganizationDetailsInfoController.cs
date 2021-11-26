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
    public class SocialOrganizationDetailsInfoController : Controller
    {
        public readonly ISocialOrganizationDetailsService SocialOrganizationDetailsService;
        public readonly ISocialOrganizationService socialOrganizationService;

        public SocialOrganizationDetailsInfoController(ISocialOrganizationDetailsService _SocialOrganizationDetailsService, ISocialOrganizationService _socialOrganizationService)
        {
            SocialOrganizationDetailsService = _SocialOrganizationDetailsService;
            socialOrganizationService = _socialOrganizationService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            SocialOrganizationDetailsViewModel model = new SocialOrganizationDetailsViewModel
            {
                socialOrganizationDetailsList = await SocialOrganizationDetailsService.GetAllSocialOrganizationDetails(),
                socialOrganizations = await socialOrganizationService.GetAllSocialOrganization(),
            };
            return View(model);
        }


        #region Api

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] SocialOrganizationDetailsViewModel model)
        {

            try
            {
                SocialOrganizationDetails data = new SocialOrganizationDetails
                {
                    Id = model.SocialOrganizationDetailsId,
                     organizationTypeName =model.organizationTypeName,  //blood,human service,child service etc
                     location =model.location,
                     establishedYear =model.establishedYear,
                     presentPresidentName =model.presentPresidentName,
                     creator =model.creator,
                     contactNo =model.contactNo,
                     SocialOrganizationId =model.SocialOrganizationId, //fk
                };

                await SocialOrganizationDetailsService.SaveSocialOrganizationDetails(data);
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
                response = await SocialOrganizationDetailsService.DeleteSocialOrganizationDetailsById(id);
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
