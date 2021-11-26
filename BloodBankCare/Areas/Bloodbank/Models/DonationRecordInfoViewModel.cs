using BloodBankCare.Data.Entity.ApplicationUsers;
using BloodBankCare.Data.Entity.Bloodbank;
using BloodBankCare.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Areas.Bloodbank.Models
{
    public class DonationRecordInfoViewModel
    {
        public int DonationRecordInfoId { get; set; }
        public string userId { get; set; }
        public string donorUserId { get; set; }
        public DateTime? requestDate { get; set; }
        public DateTime? needDate { get; set; }
        public string donationPlaceName { get; set; }
        public string location { get; set; }
        public int? amountOfBlood { get; set; }
        public string patientName { get; set; }
        public string patientAddress { get; set; }
        public string reasonOfBlood { get; set; }
        //fk
        public int? BloodGroupId { get; set; }
    
        public int? isApproved { get; set; }  //0=pending(request),1=ongoing(donar),2=approved(admin)
        public string approver { get; set; }  //approver user id or name(super Admin)

        public virtual IEnumerable<BloodGroup> bloodGroups { get; set; }
        public virtual DonationRecordInfo donationRecordInfo { get; set; }
        public virtual IEnumerable<DonationRecordInfo> donationRecordInfos { get; set; }
        public virtual IEnumerable<ApplicationUser> ApplicationUsers { get; set; }

    }
}
