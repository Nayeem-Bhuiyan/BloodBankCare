using BloodBankCare.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Areas.MasterData.Models
{
    public class BloodGroupViewModel
    {
        public int BloodGroupId { get; set; }
        public string groupName { get; set; }

        public virtual BloodGroup bloodGroup { get; set; }
        public virtual IEnumerable<BloodGroup> bloodGroups { get; set; }
    }
}
