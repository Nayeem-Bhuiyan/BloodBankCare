using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Data.Entity.Home
{
    public class GeneralFAQS:Base
    {
        public string headline { get; set; }
        public string question { get; set; }
        public string answer { get; set; }
    }
}
