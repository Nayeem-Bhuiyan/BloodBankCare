using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Data.Entity.Home
{
    public class OtherBloodBankDetails:Base
    {
        public string bloodBankName { get; set; }
        public string contactName { get; set; }
        public string email { get; set; }
        public string websiteLink { get; set; }
        public string address { get; set; }
    }
}
