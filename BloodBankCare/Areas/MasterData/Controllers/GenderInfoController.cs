using BloodBankCare.Areas.MasterData.Models;
using BloodBankCare.Data.Entity.MasterData;
using BloodBankCare.Services.MasterDataService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Areas.MasterData.Controllers
{
    [Area("MasterData")]
    public class GenderInfoController : Controller
    {
        public readonly IGenderService GenderService;

        public GenderInfoController(IGenderService _GenderService)
        {
            GenderService = _GenderService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            GenderViewModel model = new GenderViewModel
            {
                genders = await GenderService.GetAllGender(),
            };
            return View(model);
        }


        #region Api

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] GenderViewModel model)
        {

            try
            {
                Gender data = new Gender
                {
                    Id = model.GenderId,
                    genderName = model.genderName
                };

                await GenderService.SaveGender(data);
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
                response = await GenderService.DeleteGenderById(id);
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
