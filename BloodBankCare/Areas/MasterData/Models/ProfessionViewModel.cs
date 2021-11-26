using BloodBankCare.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Areas.MasterData.Models
{
    public class ProfessionViewModel
    {
        public int ProfessionId { get; set; }
        public string professionName { get; set; }


        public virtual Profession profession { get; set; }
        public virtual IEnumerable<Profession> professions { get; set; }
    }
}
