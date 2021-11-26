using BloodBankCare.Data.Entity.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Areas.Home.Models
{
    public class OtherBloodBankDetailsViewModel
    {
        public int OtherBloodBankDetailsId { get; set; }
        public string bloodBankName { get; set; }
        public string contactName { get; set; }
        public string email { get; set; }
        public string websiteLink { get; set; }
        public string address { get; set; }

        public virtual OtherBloodBankDetails otherBloodBankDetails  { get; set; }
        public virtual IEnumerable<OtherBloodBankDetails> otherBloodBankDetailsList  { get; set; }
    }
}
