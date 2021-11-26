using BloodBankCare.Areas.Complain.Models;
using BloodBankCare.Data.Entity.ApplicationUsers;
using BloodBankCare.Data.Entity.Complain;
using BloodBankCare.Services.ComplainService.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Areas.Complain.Controllers
{
    [Area("Complain")]
    public class ComplainSolveDetailsController : Controller
    {
        public readonly IComplainSolveInfoDetailsService ComplainSolveInfoDetailsService;
        public readonly IComplainInformationService ComplainInformationService;
        private readonly UserManager<ApplicationUser> _userManager;
        public readonly IComplainSolveInfoService ComplainSolveInfoService;
        public ComplainSolveDetailsController(IComplainInformationService _ComplainInformationService,IComplainSolveInfoDetailsService _ComplainSolveInfoDetailsService, UserManager<ApplicationUser> userManager, IComplainSolveInfoService _ComplainSolveInfoService)
        {
            ComplainSolveInfoDetailsService = _ComplainSolveInfoDetailsService;
            _userManager = userManager;
            ComplainSolveInfoService = _ComplainSolveInfoService;
            ComplainInformationService = _ComplainInformationService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ComplainSolveInfoDetailsViewModel model = new ComplainSolveInfoDetailsViewModel
            {
                complainSolveInfoDetails = await ComplainSolveInfoDetailsService.GetAllComplainSolveInfoDetails(),
            };
            return View(model);
        }


        #region Api

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] ComplainSolveInfoDetailsViewModel model)
        {
            var ans = 0;
            try
            {
                ApplicationUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                if (user.Id!=null)
                {
                    ComplainSolveInfoDetails data = new ComplainSolveInfoDetails
                    {
                        Id = model.ComplainSolveInfoDetailsId,
                        ComplainInformationDetailsId = model.ComplainInformationDetailsId,  //fk
                        solvedBy = user.UserName, //UserId/email who solve it
                        contact = model.contact, //mobile/email who solve it
                        solveDate = model.solveDate,
                        solveinfoHeadLine = model.solveinfoHeadLine,
                        solveinfoDetails = model.solveinfoDetails,
                    };

                    ans=await ComplainSolveInfoDetailsService.SaveComplainSolveInfoDetails(data);

                    if (ans>0)
                    {
                       
                        
                        ComplainSolveInfo obj = new ComplainSolveInfo
                        {
                            Id = 0,
                            ComplainSolveInfoDetailsId = data.Id,
                            isSolved = 1,
                            isApprovedByAdmin=0,

                        };
                        await ComplainSolveInfoService.SaveComplainSolveInfo(obj);

                       var complaininfo= await ComplainInformationService.GetComplainInfoByDetailsId(model.ComplainInformationDetailsId);
                        ComplainInformation objData = new ComplainInformation
                        {
                            Id = complaininfo.Id,
                            ComplainInformationDetailsId = complaininfo.ComplainInformationDetailsId,
                            isSolved = 1, //0=pending,1=reject,2=solved  (ComplainInformationDetails er List (reject,solve) button thakbe jar bitore ComplainInformationDetailsId included thakbe)
                                          //solve button click hole modal asbe solve details information niye  save hobe solve info table e
                            isApprovedByAdmin = 0, //0=pending(user),1=reject(admin),2=approved(admin)
                        };

                        await ComplainInformationService.SaveComplainInformation(objData);
                        return RedirectToAction(nameof(Index));
                        
                    }
                    else
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                else
                {
                    return Redirect("/Auth/Account/Login");
                }

                
            }
            catch (Exception)
            {
                throw;
            }

        }



        [HttpPost]
        public async Task<IActionResult> ComplainSolveCancel(int? id)  //complaindetailsId
        {
            var comSolveData = await ComplainSolveInfoDetailsService.GetComplainSolveInfoDetailsByComDetailsId(id);

            ComplainSolveInfo obj = new ComplainSolveInfo
            {
                Id = 0,
                ComplainSolveInfoDetailsId = comSolveData.Id,
                isSolved = 0,
                isApprovedByAdmin = 0,

            };
            await ComplainSolveInfoService.SaveComplainSolveInfo(obj);

            var complaininfo = await ComplainInformationService.GetComplainInfoByDetailsId(id);
            ComplainInformation objData = new ComplainInformation
            {
                Id = complaininfo.Id,
                ComplainInformationDetailsId = id,
                isSolved = 0, //0=pending,1=reject,2=solved  (ComplainInformationDetails er List (reject,solve) button thakbe jar bitore ComplainInformationDetailsId included thakbe)
                              //solve button click hole modal asbe solve details information niye  save hobe solve info table e
                isApprovedByAdmin = 0, //0=pending(user),1=reject(admin),2=approved(admin)
            };

            await ComplainInformationService.SaveComplainInformation(objData);
            return RedirectToAction(nameof(Index));
        }



        [HttpPost]
        public async Task<IActionResult> ComplainSolveApproval(int? id)  //complaindetailsId
        {
            var comSolveData = await ComplainSolveInfoDetailsService.GetComplainSolveInfoDetailsByComDetailsId(id);
            
            ComplainSolveInfo obj = new ComplainSolveInfo
            {
                Id = 0,
                ComplainSolveInfoDetailsId = comSolveData.Id,
                isSolved = 1,
                isApprovedByAdmin = 1,

            };
            await ComplainSolveInfoService.SaveComplainSolveInfo(obj);

            var complaininfo = await ComplainInformationService.GetComplainInfoByDetailsId(id);
            ComplainInformation objData = new ComplainInformation
            {
                Id = complaininfo.Id,
                ComplainInformationDetailsId =id,
                isSolved = 1, //0=pending,1=reject,2=solved  (ComplainInformationDetails er List (reject,solve) button thakbe jar bitore ComplainInformationDetailsId included thakbe)
                              //solve button click hole modal asbe solve details information niye  save hobe solve info table e
                isApprovedByAdmin = 1, //0=pending(user),1=reject(admin),2=approved(admin)
            };

            await ComplainInformationService.SaveComplainInformation(objData);
            return RedirectToAction(nameof(Index));
        }




        [HttpPost]
        public async Task<IActionResult> ComplainSolveReject(int? id)  //complaindetailsId
        {
            var comSolveData = await ComplainSolveInfoDetailsService.GetComplainSolveInfoDetailsByComDetailsId(id);

            ComplainSolveInfo obj = new ComplainSolveInfo
            {
                Id = 0,
                ComplainSolveInfoDetailsId = comSolveData.Id,
                isSolved = 1,
                isApprovedByAdmin = 2,

            };
            await ComplainSolveInfoService.SaveComplainSolveInfo(obj);

            var complaininfo = await ComplainInformationService.GetComplainInfoByDetailsId(id);
            ComplainInformation objData = new ComplainInformation
            {
                Id = complaininfo.Id,
                ComplainInformationDetailsId = id,
                isSolved = 1, //0=pending,1=reject,2=solved  (ComplainInformationDetails er List (reject,solve) button thakbe jar bitore ComplainInformationDetailsId included thakbe)
                              //solve button click hole modal asbe solve details information niye  save hobe solve info table e
                isApprovedByAdmin = 2, //0=pending(user),2=reject(admin),1=approved(admin)
            };

            await ComplainInformationService.SaveComplainInformation(objData);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            bool response = false;
            try
            {
                response = await ComplainSolveInfoDetailsService.DeleteComplainSolveInfoDetailsById(id);
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
