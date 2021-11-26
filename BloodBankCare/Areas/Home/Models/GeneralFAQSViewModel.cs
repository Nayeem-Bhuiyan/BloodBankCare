using BloodBankCare.Data.Entity.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Areas.Home.Models
{
    public class GeneralFAQSViewModel
    {
        public int GeneralFAQSId { get; set; }
        public string headline { get; set; }
        public string question { get; set; }
        public string answer { get; set; }

        public virtual GeneralFAQS generalFAQS { get; set; }
        public virtual IEnumerable<GeneralFAQS> generalFAQsList { get; set; }
    }
}
