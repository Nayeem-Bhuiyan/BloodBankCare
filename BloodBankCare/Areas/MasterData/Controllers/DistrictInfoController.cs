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
    public class DistrictInfoController : Controller
    {

        public readonly IDistrictService DistrictService;
        public readonly ICountryService countryService;
        public DistrictInfoController(IDistrictService _DistrictService, ICountryService _countryService)
        {
            DistrictService = _DistrictService;
            countryService = _countryService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DistrictViewModel model = new DistrictViewModel
            {
                districts = await DistrictService.GetAllDistrict(),
                countries = await countryService.GetAllCountry()
            };
            return View(model);
        }



        #region Api

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] DistrictViewModel model)
        {

            try
            {
                District data = new District
                {
                    Id = model.DistrictId,
                    districtName = model.districtName,
                    CountryId=model.CountryId
                };

                await DistrictService.SaveDistrict(data);
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
                response = await DistrictService.DeleteDistrictById(id);
            }
            catch (Exception ex)
            {
                throw ex;

            }

            return Json(response);
        }



        [HttpGet]
        public async Task<IActionResult> GetDistrictListByCountryId(int? id)
        {
            return Json(await DistrictService.GetDistrictsByCountryId(id));
        }

        #endregion
    }
}
