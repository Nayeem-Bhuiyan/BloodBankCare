using BloodBankCare.Data.Entity.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Areas.Home.Models
{
    public class HospitalDetailsViewModel
    {
        public int HospitalDetailsId { get; set; }
        public string hospitalName { get; set; }
        public string contactNo { get; set; }
        public string email { get; set; }
        public string websiteLink { get; set; }
        public string address { get; set; }

        public virtual HospitalDetails hospitalDetails { get; set; }
        public virtual IEnumerable<HospitalDetails> hospitalDetailsList { get; set; }
    }
}
