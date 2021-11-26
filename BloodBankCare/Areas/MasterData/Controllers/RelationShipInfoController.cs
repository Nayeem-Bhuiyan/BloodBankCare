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
    public class RelationShipInfoController : Controller
    {
        public readonly IRelationShipService RelationShipService;

        public RelationShipInfoController(IRelationShipService _RelationShipService)
        {
            RelationShipService = _RelationShipService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            RelationShipViewModel model = new RelationShipViewModel
            {
                relationShips = await RelationShipService.GetAllRelationShip(),
            };
            return View(model);
        }


        #region Api

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] RelationShipViewModel model)
        {

            try
            {
                RelationShip data = new RelationShip
                {
                    Id = model.RelationShipId,
                    relationShipName = model.relationShipName
                };

                await RelationShipService.SaveRelationShip(data);
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
                response = await RelationShipService.DeleteRelationShipById(id);
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
