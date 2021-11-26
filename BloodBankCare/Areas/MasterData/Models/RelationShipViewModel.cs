using BloodBankCare.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Areas.MasterData.Models
{
    public class RelationShipViewModel
    {
        public int RelationShipId { get; set; }
        public string relationShipName { get; set; }
        public virtual RelationShip relationShip { get; set; }
        public virtual IEnumerable<RelationShip> relationShips { get; set; }
    }
}
