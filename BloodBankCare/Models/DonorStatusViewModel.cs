using BloodBankCare.Data.Entity.ApplicationUsers;
using BloodBankCare.Data.Entity.Bloodbank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Models
{
    public class DonorStatusViewModel
    {
        public virtual DonorInformation donorInformation { get; set; }
        public string donationStatus { get; set; }
        public int lastDonationDaysCount { get; set; }
        public string donorType { get; set; }  //Experienced Donor,Fresh Donor,
        public string donorStatus { get; set; }
        public int OrderByRecord { get; set; }
        public int? totalDonation { get; set; }
        public DateTime? LastDonatedDate { get; set; }
        public string donorUserId { get; set; }
    }
}
