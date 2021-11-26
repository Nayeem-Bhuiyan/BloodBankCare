using BloodBankCare.Areas.Home.Models;
using BloodBankCare.Data.Entity.Home;
using BloodBankCare.Services.HomeService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Areas.Home.Controllers
{
   [Area("Home")]
    public class OtherBloodBanksController : Controller
    {
        public readonly IOtherBloodBankDetailsService OtherBloodBankDetailsService;

        public OtherBloodBanksController(IOtherBloodBankDetailsService _OtherBloodBankDetailsService)
        {
            OtherBloodBankDetailsService = _OtherBloodBankDetailsService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            OtherBloodBankDetailsViewModel model = new OtherBloodBankDetailsViewModel
            {
                otherBloodBankDetailsList = await OtherBloodBankDetailsService.GetAllOtherBloodBankDetails(),
            };
            return View(model);
        }


        #region Api

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] OtherBloodBankDetailsViewModel model)
        {

            try
            {
                OtherBloodBankDetails data = new OtherBloodBankDetails
                {
                     Id = model.OtherBloodBankDetailsId,
                     bloodBankName =model.bloodBankName,
                     contactName =model.contactName,
                     email =model.email,
                     websiteLink =model.websiteLink,
                     address =model.address,
                };

                await OtherBloodBankDetailsService.SaveOtherBloodBankDetails(data);
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
                response = await OtherBloodBankDetailsService.DeleteOtherBloodBankDetailsById(id);
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
