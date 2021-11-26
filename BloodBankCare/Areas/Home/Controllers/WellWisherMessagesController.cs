using BloodBankCare.Areas.Home.Models;
using BloodBankCare.Data.Entity.ApplicationUsers;
using BloodBankCare.Data.Entity.Home;
using BloodBankCare.Helpers;
using BloodBankCare.Services.HomeService.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Areas.Home.Controllers
{
    [Area("Home")]
    public class WellWisherMessagesController : Controller
    {
        public readonly IWellWisherMessageService _WellWisherMessageService;
        private readonly UserManager<ApplicationUser> _userManager;

        public WellWisherMessagesController(IWellWisherMessageService WellWisherMessageService, UserManager<ApplicationUser> userManager)
        {
            _WellWisherMessageService = WellWisherMessageService;
            _userManager = userManager;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            WellWisherMessageViewModel model = new WellWisherMessageViewModel
            {
                wellWisherMessages = await _WellWisherMessageService.GetAllWellWisherMessage(),
            };
            return View(model);
        }


        #region Api

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] WellWisherMessageViewModel model)
        {

            try
            {



                string UploadedImageUrl = "DefaultImage/NoImage.jpg";

                string fileName;
                string message = FileSave.SaveWellWisherImage(out fileName, model.ImageFile);
                if (message == "success")
                {
                    UploadedImageUrl = "";
                    UploadedImageUrl = fileName;
                }
                else
                {
                    return View(model);
                }

      

                    WellWisherMessage data = new WellWisherMessage
                    {
                        Id = model.WellWisherMessageId,
                        personName=model.personName,
                        organizationName=model.organizationName,
                        designation=model.designation,
                        headlineText=model.headlineText,
                        detailsMessage=model.detailsMessage,
                        imgmUrl= UploadedImageUrl
                    };
                    await _WellWisherMessageService.SaveWellWisherMessage(data);
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
                response = await _WellWisherMessageService.DeleteWellWisherMessageById(id);
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
