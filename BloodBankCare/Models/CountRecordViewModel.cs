using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Models
{
    public class CountRecordViewModel
    {
        public int? totalDonation { get; set; }
        public int? totalDonationOfCurrentMonth { get; set; }
        public int? totalUsers { get; set; }
        public int? totalDonors { get; set; }
        public int? totalDonatedDonors { get; set; }
    }
}
