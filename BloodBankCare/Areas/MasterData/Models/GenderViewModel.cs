using BloodBankCare.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Areas.MasterData.Models
{
    public class GenderViewModel
    {

        public int GenderId { get; set; }
        public string genderName { get; set; }


        public virtual Gender gender { get; set; }
        public virtual IEnumerable<Gender> genders { get; set; }
    }
}
