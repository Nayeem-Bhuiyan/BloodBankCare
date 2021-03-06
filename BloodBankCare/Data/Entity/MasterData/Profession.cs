using BloodBankCare.Data.Entity.Bloodbank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Data.Entity.MasterData
{
    public class Profession:Base
    {
        public string professionName { get; set; }

        public virtual IEnumerable<DonorInformation> DonorInformations { get; set; }
    }
}
