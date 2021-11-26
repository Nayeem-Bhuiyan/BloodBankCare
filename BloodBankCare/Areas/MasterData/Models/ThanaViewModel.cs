using BloodBankCare.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Areas.MasterData.Models
{
    public class ThanaViewModel
    {
        public int ThanaId { get; set; }
        public string thanaName { get; set; }
        //fk
        public int? DistrictId { get; set; }

        public virtual Thana thana { get; set; }
        public virtual IEnumerable<Thana> thanas { get; set; }
        public virtual IEnumerable<District> districts { get; set; }
    }
}
