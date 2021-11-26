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
    public class ComplainSolveInformationController : Controller
    {
        public readonly IComplainSolveInfoService ComplainSolveInfoService;
        public readonly IComplainInformationDetailsService ComplainInformationDetailsService;
        public readonly IComplainInformationService complainInformationService;

        public ComplainSolveInformationController(IComplainSolveInfoService _ComplainSolveInfoService, IComplainInformationDetailsService _ComplainInformationDetailsService, IComplainInformationService _complainInformationService)
        {
            ComplainSolveInfoService = _ComplainSolveInfoService;
            ComplainInformationDetailsService = _ComplainInformationDetailsService;
            complainInformationService = _complainInformationService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ComplainSolveInfoViewModel model = new ComplainSolveInfoViewModel
            {
                complainSolveInfos = await ComplainSolveInfoService.GetAllComplainSolveInfo(),
            };
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> ComplainSolve()
        {
            ComplainInformationDetailsViewModel model = new ComplainInformationDetailsViewModel
            {
     
                complainInformations = await complainInformationService.GetAllComplainInformation(),
            };
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> ComplainApprove()
        {


          var dataList=  await complainInformationService.GetAllComplainInformation();
            dataList.Where(x => x.isApprovedByAdmin == 0 && x.isApprovedByAdmin == 2).ToList();

            ComplainInformationDetailsViewModel model = new ComplainInformationDetailsViewModel
            {

                complainInformations =dataList,
            };
            return View(model);
        }


        #region Api

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] ComplainSolveInfoViewModel model)
        {

            try
            {
                ComplainSolveInfo data = new ComplainSolveInfo
                {
                    Id = model.ComplainSolveInfoId,
                    ComplainSolveInfoDetailsId=model.ComplainSolveInfoDetailsId,  //fk
                    isSolved=0,
                    isApprovedByAdmin=0,
                };

                await ComplainSolveInfoService.SaveComplainSolveInfo(data);
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
                response = await ComplainSolveInfoService.DeleteComplainSolveInfoById(id);
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
