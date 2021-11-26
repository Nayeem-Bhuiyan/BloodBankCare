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
    public class ThanaInfoController : Controller
    {
        public readonly IThanaService ThanaService;
        public readonly IDistrictService DistrictService;
        public ThanaInfoController(IThanaService _ThanaService, IDistrictService _DistrictService)
        {
            ThanaService = _ThanaService;
            DistrictService = _DistrictService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ThanaViewModel model = new ThanaViewModel
            {
                thanas = await ThanaService.GetAllThana(),
                districts = await DistrictService.GetAllDistrict()
            };
            return View(model);
        }


        #region Api

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] ThanaViewModel model)
        {

            try
            {
                Thana data = new Thana
                {
                    Id = model.ThanaId,
                    thanaName = model.thanaName,
                    DistrictId = model.DistrictId
                };

                await ThanaService.SaveThana(data);
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
                response = await ThanaService.DeleteThanaById(id);
            }
            catch (Exception ex)
            {
                throw ex;

            }

            return Json(response);
        }


        [HttpGet]
        public async Task<IActionResult> GetThanaListByDistrictId(int? id)
        { 
        return Json(await ThanaService.GetThanasByDistrictId(id));
        }

        #endregion
    }
}
