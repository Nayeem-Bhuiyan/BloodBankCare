using BloodBankCare.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Data.Entity.Bloodbank
{
    public class BloodRequestInfo:Base
    {
        public string userId { get; set; }      //requestUserId
        public string patientName { get; set; }
        public string patientAddress { get; set; }
        public string reasonOfBlood { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? requestDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? needDate { get; set; }
        public string donationPlaceName { get; set; }
        public string location { get; set; }
        public int? amountOfBlood { get; set; }

        //fk
        public int RelationShipId { get; set; }
        public virtual RelationShip RelationShip { get; set; }

        public int? BloodGroupId { get; set; }
        public virtual BloodGroup BloodGroup { get; set; }


        public virtual IEnumerable<BloodRequestReceivedInfo> BloodRequestReceivedInfos { get; set; }


    }
}
