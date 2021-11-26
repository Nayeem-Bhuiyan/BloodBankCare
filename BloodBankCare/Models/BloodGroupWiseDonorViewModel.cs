using BloodBankCare.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Models
{
    public class BloodGroupWiseDonorViewModel
    {
        public virtual BloodGroup bloodGroup { get; set; }
        public int TotalDonor  { get; set; }
        public int TotalDonation { get; set; }
        public int bloodGroupId { get; set; }
    }
}
