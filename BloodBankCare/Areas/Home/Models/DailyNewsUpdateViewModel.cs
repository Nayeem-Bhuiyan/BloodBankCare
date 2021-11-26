using BloodBankCare.Data.Entity.Home;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Areas.Home.Models
{
    public class DailyNewsUpdateViewModel
    {
        public int DailyNewsUpdateId { get; set; }
        public string headingText { get; set; }
        public string detailsDescription { get; set; }
        public string location { get; set; }
        public string reason { get; set; }
        public DateTime? date { get; set; }
        public string fileUrl { get; set; }
        public IFormFile UploadFile { get; set; }

        public virtual DailyNewsUpdate dailyNewsUpdate { get; set; }
        public virtual IEnumerable<DailyNewsUpdate> dailyNewsUpdates { get; set; }
    }
}
