using BloodBankCare.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Areas.MasterData.Models
{
    public class ReligionViewModel
    {
        public int ReligionId { get; set; }
        public string religionName { get; set; }


        public virtual Religion religion { get; set; }
        public virtual IEnumerable<Religion> religions { get; set; }
    }
}
