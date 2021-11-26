using BloodBankCare.Data.Entity.NoticeBoard;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Areas.NoticeBoard.Models
{
    public class NoticeBoardInfoViewModel
    {
        public int NoticeBoardInfoId { get; set; }
        public string headingText { get; set; }
        public string detailsDescription { get; set; }
        public string fileUrl { get; set; }
        public string Uploaded { get; set; }
        public IFormFile UploadedFile { get; set; }
        public DateTime? publishDate { get; set; }
        public DateTime? endDate { get; set; }

        public virtual NoticeBoardInfo noticeBoardInfo { get; set; }
        public virtual IEnumerable<NoticeBoardInfo> noticeBoardInfos { get; set; }
    }
}
