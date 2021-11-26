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
    public class ProfessionInfoController : Controller
    {
        public readonly IProfessionService ProfessionService;

        public ProfessionInfoController(IProfessionService _ProfessionService)
        {
            ProfessionService = _ProfessionService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ProfessionViewModel model = new ProfessionViewModel
            {
                professions = await ProfessionService.GetAllProfession(),
            };
            return View(model);
        }


        #region Api

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] ProfessionViewModel model)
        {

            try
            {
                Profession data = new Profession
                {
                    Id = model.ProfessionId,
                    professionName = model.professionName
                };

                await ProfessionService.SaveProfession(data);
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
                response = await ProfessionService.DeleteProfessionById(id);
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
