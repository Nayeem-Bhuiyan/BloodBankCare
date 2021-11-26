using BloodBankCare.Areas.Home.Models;
using BloodBankCare.Data.Entity.ApplicationUsers;
using BloodBankCare.Data.Entity.Home;
using BloodBankCare.Helpers;
using BloodBankCare.Services.HomeService.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;



namespace BloodBankCare.Areas.Home.Controllers
{
    [Area("Home")]
    public class PhotoGallerysController : Controller
    {
        public readonly IPhotoGalleryService _photoGalleryService;
        private readonly UserManager<ApplicationUser> _userManager;

        public PhotoGallerysController(IPhotoGalleryService photoGalleryService, UserManager<ApplicationUser> userManager)
        {
            _photoGalleryService = photoGalleryService;
            _userManager = userManager;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            PhotoGalleryViewModel model = new PhotoGalleryViewModel
            {
                photoGalleries = await _photoGalleryService.GetAllPhotoGallery(),
            };
            return View(model);
        }


        #region Api

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] PhotoGalleryViewModel model)
        {

            try
            {



                string ImageUrl= "DefaultImage/NoImage.jpg";

                string fileName;
                string message = FileSave.SavePhotoGalleryImage(out fileName, model.Image);
                if (message == "success")
                {
                    ImageUrl = "";
                    ImageUrl = fileName;
                }
                else
                {
                    return View(model);
                }

                if (User.Identity.Name == null)
                {
                    return Redirect("~/Auth/Account/Login");
                    
                }
                else
                {

                    ApplicationUser user = await _userManager.FindByNameAsync(User.Identity.Name);

                    PhotoGallery data = new PhotoGallery
                    {
                        Id = model.PhotoGalleryId,
                        userId= user.Id,
                        fileUrl=ImageUrl,
                        fileType=model.fileType,
                        headLineText=model.headLineText,
                        footerText=model.footerText,
                        DisplaySectionName=model.DisplaySectionName


                    };
                    await _photoGalleryService.SavePhotoGallery(data);
                    return RedirectToAction(nameof(Index));
                }

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
                response = await _photoGalleryService.DeletePhotoGalleryById(id);
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
