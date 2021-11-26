using BloodBankCare.Data.Entity.Home;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Areas.Home.Models
{
    public class WellWisherMessageViewModel
    {
        public int WellWisherMessageId { get; set; }
        public string personName { get; set; }
        public string organizationName { get; set; }
        public string designation { get; set; }
        public string headlineText { get; set; }
        public string detailsMessage { get; set; }
        public string imgmUrl { get; set; }
        public IFormFile ImageFile { get; set; }

        public virtual WellWisherMessage wellWisherMessage { get; set; }
        public virtual IEnumerable<WellWisherMessage> wellWisherMessages { get; set; }
    }
}
