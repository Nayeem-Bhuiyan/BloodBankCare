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
    public class DesignationInfoController : Controller
    {
        public readonly IDesignationService DesignationService;

        public DesignationInfoController(IDesignationService _DesignationService)
        {
            DesignationService = _DesignationService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DesignationViewModel model = new DesignationViewModel
            {
                designations = await DesignationService.GetAllDesignation(),
            };
            return View(model);
        }


        #region Api

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] DesignationViewModel model)
        {

            try
            {
                Designation data = new Designation
                {
                    Id = model.DesignationId,
                    designationName = model.designationName
                };

                await DesignationService.SaveDesignation(data);
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
                response = await DesignationService.DeleteDesignationById(id);
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
