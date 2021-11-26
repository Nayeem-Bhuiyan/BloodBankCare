using BloodBankCare.Data.Entity.Bloodbank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Models
{
    public class AdminDashBoardViewModel
    {
        public CountRecordViewModel countRecordInfos { get; set; }
        public IEnumerable<DonorInformation> donors { get; set; }
        public IEnumerable<BloodGroupWiseDonorViewModel> groupWiseDonors { get; set; }
        public DonationRecordInfo lastDonationRecord { get; set; }
        public IEnumerable<DonationRecordInfo>CurrentMonthDonations { get; set; }
        public IEnumerable<DonationRecordInfo>LastMonthDonations { get; set; }
    }
}
