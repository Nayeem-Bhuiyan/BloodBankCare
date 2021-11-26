using BloodBankCare.Areas.Home.Models;
using BloodBankCare.Data.Entity.Home;
using BloodBankCare.Services.HomeService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Areas.Home.Controllers
{
   [Area("Home")]
    public class ContactUsInfoController : Controller
    {
        public readonly IContactUsService ContactUSService;

        public ContactUsInfoController(IContactUsService _ContactUSService)
        {
            ContactUSService = _ContactUSService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ContactUsViewModel model = new ContactUsViewModel
            {
                contactUSList = await ContactUSService.GetAllContactUS(),
            };
            return View(model);
        }


        #region Api

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] ContactUsViewModel model)
        {

            try
            {
                ContactUS data = new ContactUS
                {
                    Id = model.ContactUsId,
                     name =model.name,
                    email =model.email,
                   contactNo =model.contactNo,
                   message =model.message,
                   date =model.date
                };

                await ContactUSService.SaveContactUS(data);
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
                response = await ContactUSService.DeleteContactUSById(id);
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
