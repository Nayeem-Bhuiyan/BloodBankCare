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
    public class ReligionInfoController : Controller
    {
        public readonly IReligionService ReligionService;

        public ReligionInfoController(IReligionService _ReligionService)
        {
            ReligionService = _ReligionService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ReligionViewModel model = new ReligionViewModel
            {
                religions = await ReligionService.GetAllReligion(),
            };
            return View(model);
        }


        #region Api

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] ReligionViewModel model)
        {

            try
            {
                Religion data = new Religion
                {
                    Id = model.ReligionId,
                    religionName = model.religionName
                };

                await ReligionService.SaveReligion(data);
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
                response = await ReligionService.DeleteReligionById(id);
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
