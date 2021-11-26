using BloodBankCare.Areas.GeneralFAQ.Models;
using BloodBankCare.Data.Entity.Home;
using BloodBankCare.Services.HomeService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Areas.GeneralFAQ.Controllers
{
    [Area("GeneralFAQ")]
    public class GeneralFAQSInfoController : Controller
    {
        public readonly IGeneralFAQsService _generalFAQsService;

        public GeneralFAQSInfoController(IGeneralFAQsService generalFAQsService)
        {
            _generalFAQsService = generalFAQsService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            GeneralFAQSViewModel model = new GeneralFAQSViewModel
            {
                generalFAQs = await _generalFAQsService.GetAllGeneralFAQS()
            };
            return View(model);
        }


        #region Api

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] GeneralFAQSViewModel model)
        {

            try
            {
                GeneralFAQS data = new GeneralFAQS
                {
                    Id = model.GeneralFAQSId,
                    headline =model.headline,
                    question =model.question,
                    answer =model.answer
                };

                await _generalFAQsService.SaveGeneralFAQS(data);
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
                response = await _generalFAQsService.DeleteGeneralFAQSById(id);
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
