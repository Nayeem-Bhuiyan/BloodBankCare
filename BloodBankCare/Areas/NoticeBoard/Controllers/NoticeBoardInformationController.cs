using BloodBankCare.Areas.NoticeBoard.Models;
using BloodBankCare.Data.Entity.NoticeBoard;
using BloodBankCare.Helpers;
using BloodBankCare.Services.NoticeBoardService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Areas.NoticeBoard.Controllers
{
    [Area("NoticeBoard")]
    public class NoticeBoardInformationController : Controller
    {
        public readonly INoticeBoardInfoService NoticeBoardInfoService;

        public NoticeBoardInformationController(INoticeBoardInfoService _NoticeBoardInfoService)
        {
            NoticeBoardInfoService = _NoticeBoardInfoService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            NoticeBoardInfoViewModel model = new NoticeBoardInfoViewModel
            {
                noticeBoardInfos = await NoticeBoardInfoService.GetAllNoticeBoardInfo(),
            };
            return View(model);
        }



        [HttpGet]
        public async Task<FileResult> OpenPdfFile(int id)
        {
            
            var noticeBoardData=await NoticeBoardInfoService.GetNoticeBoardInfoById(id) ;
            string ReportURL = System.Environment.CurrentDirectory+ "/wwwroot/" +noticeBoardData.fileUrl;
            byte[] FileBytes = System.IO.File.ReadAllBytes(ReportURL);
            return File(FileBytes, "application/pdf");
        }


        #region Api

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] NoticeBoardInfoViewModel model)
        {

            try
            {


                string NoticeFile = "DefaultImage/NoImage.jpg";

                string fileName;
                string message = FileSave.SaveNoticeBoardFile(out fileName, model.UploadedFile);
                if (message == "success")
                {
                    NoticeFile = "";
                    NoticeFile = fileName;
                }
                else
                {
                    return View(model);
                }


                NoticeBoardInfo data = new NoticeBoardInfo
                {
                     Id = model.NoticeBoardInfoId,
                     headingText =model.headingText,
                     detailsDescription =model.detailsDescription,
                     publishDate =DateTime.Now,
                     endDate =model.endDate,
                     fileUrl = NoticeFile
                };

                await NoticeBoardInfoService.SaveNoticeBoardInfo(data);
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
                response = await NoticeBoardInfoService.DeleteNoticeBoardInfoById(id);
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
