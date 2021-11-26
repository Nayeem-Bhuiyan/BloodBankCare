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
    public class BloodGroupInfoController : Controller
    {

        public readonly IBloodGroupService bloodGroupService;

        public BloodGroupInfoController(IBloodGroupService _bloodGroupService)
        {
            bloodGroupService = _bloodGroupService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            BloodGroupViewModel model = new BloodGroupViewModel
            {
                bloodGroups = await bloodGroupService.GetAllBloodGroup(),
            };
            return View(model);
        }


        #region Api

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] BloodGroupViewModel model)
        {
            
            try
            {
                BloodGroup data = new BloodGroup
                {
                    Id = model.BloodGroupId,
                    groupName=model.groupName
                };
               
                 await  bloodGroupService.SaveBloodGroup(data);
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
            bool response=false;
            try
            {
                response= await bloodGroupService.DeleteBloodGroupById(id);
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
