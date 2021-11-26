using BloodBankCare.Data.Entity.ApplicationUsers;
using BloodBankCare.Data.Entity.Bloodbank;
using BloodBankCare.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Areas.Bloodbank.Models
{
    public class BloodRequestInfoViewModel
    {

        public int BloodRequestInfoId { get; set; }
        public string userId { get; set; }      //requestUserId
        public string patientName { get; set; }
        public string patientAddress { get; set; }
        public string reasonOfBlood { get; set; }
        public DateTime? requestDate { get; set; }
        public DateTime? needDate { get; set; }
        public string donationPlaceName { get; set; }
        public string location { get; set; }
        public string remarks { get; set; }
        public int? amountOfBlood { get; set; }
      //fk
        public int? BloodGroupId { get; set; }
        public int RelationShipId { get; set; }

        public virtual IEnumerable<BloodRequestInfo> bloodRequestInfos { get; set; }
        public virtual BloodRequestInfo bloodRequestInfo { get; set; }

        public virtual IEnumerable<BloodGroup> bloodGroups { get; set; }
        public virtual IEnumerable<RelationShip> relationShips { get; set; }

        public virtual ApplicationUser CurrentUser { get; set; }
    }
}
