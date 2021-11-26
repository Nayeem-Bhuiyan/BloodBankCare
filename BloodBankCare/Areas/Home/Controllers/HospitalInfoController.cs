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
    public class HospitalInfoController : Controller
    {
        public readonly IHospitalDetailsService HospitalDetailsService;

        public HospitalInfoController(IHospitalDetailsService _HospitalDetailsService)
        {
            HospitalDetailsService = _HospitalDetailsService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            HospitalDetailsViewModel model = new HospitalDetailsViewModel
            {
                hospitalDetailsList = await HospitalDetailsService.GetAllHospitalDetails(),
            };
            return View(model);
        }


        #region Api

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] HospitalDetailsViewModel model)
        {

            try
            {
                HospitalDetails data = new HospitalDetails
                {
                      Id = model.HospitalDetailsId,
                      hospitalName=model.hospitalName,
                     contactNo=model.contactNo,
                     email=model.email,
                     websiteLink=model.websiteLink,
                     address=model.address,
                };

                await HospitalDetailsService.SaveHospitalDetails(data);
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
                response = await HospitalDetailsService.DeleteHospitalDetailsById(id);
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
