using BloodBankCare.Areas.Home.Models;
using BloodBankCare.Data.Entity.Home;
using BloodBankCare.Helpers;
using BloodBankCare.Services.HomeService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Areas.Home.Controllers
{
  [Area("Home")]
    public class BloodCampaignsController : Controller
    {
        public readonly IBloodCampaignService BloodCampaignService;

        public BloodCampaignsController(IBloodCampaignService _BloodCampaignService)
        {
            BloodCampaignService = _BloodCampaignService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            BloodCampaignViewModel model = new BloodCampaignViewModel
            {
                bloodCampaigns = await BloodCampaignService.GetAllBloodCampaign(),
            };
            return View(model);
        }


        #region Api

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] BloodCampaignViewModel model)
        {

            try
            {


                string ImageUrl = "DefaultImage/NoImage.jpg";

                string fileName;
                string message = FileSave.SaveBloodCampaignImage(out fileName, model.UploadImageUrl);
                if (message == "success")
                {
                    ImageUrl = "";
                    ImageUrl = fileName;
                }
                else
                {
                    return View(model);
                }


                BloodCampaign data = new BloodCampaign
                {
                    Id = model.BloodCampaignId,
                    chiefGuest=model.chiefGuest,
                  specialGuest=model.specialGuest,
                   headingText=model.headingText,
            deatilsDescription=model.deatilsDescription,
                      location=model.location,
                      occasion=model.occasion,
                          date=model.date,
                      imageUrl= ImageUrl
                };

                await BloodCampaignService.SaveBloodCampaign(data);
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
                response = await BloodCampaignService.DeleteBloodCampaignById(id);
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
