using BloodBankCare.Areas.Home.Models;
using BloodBankCare.Data.Entity.Home;
using BloodBankCare.Helpers;
using BloodBankCare.Services.HomeService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Areas.Home.Controllers
{
    [Area("Home")]
    public class DailyNewsUpdateIsController : Controller
    {
        public readonly IDailyNewsUpdateService DailyNewsUpdateService;

        public DailyNewsUpdateIsController(IDailyNewsUpdateService _DailyNewsUpdateService)
        {
            DailyNewsUpdateService = _DailyNewsUpdateService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DailyNewsUpdateViewModel model = new DailyNewsUpdateViewModel
            {
                dailyNewsUpdates = await DailyNewsUpdateService.GetAllDailyNewsUpdate(),
            };
            return View(model);
        }


        #region Api

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] DailyNewsUpdateViewModel model)
        {

            try
            {


                string uploadedFile = "DefaultImage/NoImage.jpg";

                string fileName;
                string message = FileSave.SaveDailyNewsUpdateFile(out fileName, model.UploadFile);
                if (message == "success")
                {
                    uploadedFile = "";
                    uploadedFile = fileName;
                }
                else
                {
                    return View(model);
                }


                DailyNewsUpdate data = new DailyNewsUpdate
                {
                    Id = model.DailyNewsUpdateId,
                    headingText=model.headingText,
                    detailsDescription=model.detailsDescription,
                    location=model.location,
                     reason=model.reason,
                     date=model.date,
                     fileUrl = uploadedFile
                };

                await DailyNewsUpdateService.SaveDailyNewsUpdate(data);
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
                response = await DailyNewsUpdateService.DeleteDailyNewsUpdateById(id);
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
