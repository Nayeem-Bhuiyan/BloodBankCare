using BloodBankCare.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Data.Entity.Bloodbank
{
    public class DonationRecordInfo:Base
    {
        public string userId { get; set; }      //userId==request UserId
        public string donorUserId { get; set; }    //donorUserId=accepted by
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? requestDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? needDate { get; set; }
        public string donationPlaceName { get; set; }
        public string location { get; set; }
        public int? amountOfBlood { get; set; }
        public string patientName { get; set; }
        public string patientAddress { get; set; }
        public string reasonOfBlood { get; set; }
        public int? BloodRequestInfoId { get; set; }
        //fk
        public int? BloodGroupId { get; set; }
        public virtual BloodGroup BloodGroup { get; set; }


        public int? isApproved { get; set; }  //0=pending(request),1=approved(Admin),2=Rejected(admin)
        public string approver { get; set; }  //approver user id or name(super Admin)
    }
}
