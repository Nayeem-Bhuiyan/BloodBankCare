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
    public class ComplainInfoDetailsController : Controller
    {
        public readonly IComplainInformationDetailsService ComplainInformationDetailsService;
        public readonly IComplainInformationService complainInformationService;
        public ComplainInfoDetailsController(IComplainInformationDetailsService _ComplainInformationDetailsService, IComplainInformationService _complainInformationService)
        {
            ComplainInformationDetailsService = _ComplainInformationDetailsService;
            complainInformationService = _complainInformationService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ComplainInformationDetailsViewModel model = new ComplainInformationDetailsViewModel
            {
                complainInformationDetails = await ComplainInformationDetailsService.GetAllComplainInformationDetails(),
                complainInformations = await complainInformationService.GetAllComplainInformation(),
            };
            return View(model);
        }


        #region Api

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] ComplainInformationDetailsViewModel model)
        {
            int ans = 0;
            try
            {
                ComplainInformationDetails data = new ComplainInformationDetails
                {
                    Id = model.ComplainInformationDetailsId,
                    complainBy =model.complainBy,  //Name 
                    complainHeadLine =model.complainHeadLine,
                    complainDeatils =model.complainDeatils,
                    email =model.email,
                    contact =model.contact,
                    comPlainDate =DateTime.Now,
                    Address =model.Address,
                };

               ans= await ComplainInformationDetailsService.SaveComplainInformationDetails(data);
                if (ans>0)
                {
                  var complainInfo=  await complainInformationService.GetComplainInfoByDetailsId(data.Id);
                    
                    ComplainInformation complainData = new ComplainInformation
                    {
                        
                        Id= complainInfo!=null? complainInfo.Id:0,
                        ComplainInformationDetailsId = data.Id,
                        isSolved = 0, //0=pending,1=reject,2=solved  (ComplainInformationDetails er List (reject,solve) button thakbe jar bitore ComplainInformationDetailsId included thakbe)
                                      //solve button click hole modal asbe solve details information niye  save hobe solve info table e
                        isApprovedByAdmin = 0, //0=pending(user),1=reject(admin),2=approved(admin)
                    };

                    await complainInformationService.SaveComplainInformation(complainData);
                }
                
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
                await complainInformationService.DeleteComplainInfoByDetailsId(id);
                response = await ComplainInformationDetailsService.DeleteComplainInformationDetailsById(id);
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
