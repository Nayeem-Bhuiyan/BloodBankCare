using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Data.Entity.Home
{
    public class WellWisherMessage:Base
    {
        public string personName { get; set; }
        public string organizationName { get; set; }
        public string designation { get; set; }
        public string headlineText { get; set; }
        public string detailsMessage { get; set; }
        public string imgmUrl { get; set; }
    }
}
