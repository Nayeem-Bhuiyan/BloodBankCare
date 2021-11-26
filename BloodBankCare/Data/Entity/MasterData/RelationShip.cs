using BloodBankCare.Data.Entity.Bloodbank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Data.Entity.MasterData
{
    public class RelationShip:Base
    {
        public string  relationShipName { get; set; }

        public virtual IEnumerable<BloodRequestInfo> BloodRequestInfos { get; set; }
    }
}
