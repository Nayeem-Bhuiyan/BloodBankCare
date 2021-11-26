using BloodBankCare.Data.Entity.Home;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Areas.Home.Models
{
    public class BloodCampaignViewModel
    {
        public int BloodCampaignId { get; set; }
        public string chiefGuest { get; set; }
        public string specialGuest { get; set; }
        public string headingText { get; set; }
        public string deatilsDescription { get; set; }
        public string location { get; set; }
        public string occasion { get; set; }
        public DateTime? date { get; set; }
        public string imageUrl { get; set; }
        public IFormFile UploadImageUrl { get; set; }

        public virtual BloodCampaign bloodCampaign { get; set; }
        public virtual IEnumerable<BloodCampaign> bloodCampaigns { get; set; }
    }
}
