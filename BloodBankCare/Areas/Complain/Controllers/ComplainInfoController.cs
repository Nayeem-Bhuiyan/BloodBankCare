using BloodBankCare.Areas.Complain.Models;
using BloodBankCare.Data.Entity.Complain;
using BloodBankCare.Services.ComplainService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Areas.Complain.Controllers
{
    [Area("Complain")]
    public class ComplainInfoController : Controller
    {
        public readonly IComplainInformationService ComplainInformationService;
        public readonly IComplainInformationDetailsService ComplainInformationDetailsService;
        public readonly IComplainInformationService complainInformationService;
        public ComplainInfoController(IComplainInformationService _ComplainInformationService, IComplainInformationDetailsService _ComplainInformationDetailsService, IComplainInformationService _complainInformationService)
        {
            ComplainInformationService = _ComplainInformationService;
            ComplainInformationDetailsService = _ComplainInformationDetailsService;
            complainInformationService = _complainInformationService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ComplainInformationViewModel model = new ComplainInformationViewModel
            {
                complainInformations = await ComplainInformationService.GetAllComplainInformation(),
            };
            return View(model);
        }


        #region Api

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] ComplainInformationViewModel model)
        {
            //complain info table er ak e sathe ei table data dokbe
            try
            {
                ComplainInformation data = new ComplainInformation
                {
                        Id = model.ComplainInformationId,
                        ComplainInformationDetailsId =model.ComplainInformationDetailsId,
                        isSolved =0, //0=pending,1=reject,2=solved  (ComplainInformationDetails er List (reject,solve) button thakbe jar bitore ComplainInformationDetailsId included thakbe)
                                     //solve button click hole modal asbe solve details information niye  save hobe solve info table e
                        isApprovedByAdmin =0, //0=pending(user),1=reject(admin),2=approved(admin)
                };

                await ComplainInformationService.SaveComplainInformation(data);
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
                response = await ComplainInformationService.DeleteComplainInformationById(id);
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
