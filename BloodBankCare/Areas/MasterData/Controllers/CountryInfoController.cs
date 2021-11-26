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
    public class CountryInfoController : Controller
    {
        public readonly ICountryService countryService;

        public CountryInfoController(ICountryService _countryService)
        {
            countryService = _countryService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            CountryViewModel model = new CountryViewModel
            {
                countries = await countryService.GetAllCountry(),
            };
            return View(model);
        }


        #region Api

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] CountryViewModel model)
        {

            try
            {
                Country data = new Country
                {
                    Id = model.CountryId,
                    countryName = model.countryName
                };

                await countryService.SaveCountry(data);
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
                response = await countryService.DeleteCountryById(id);
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
