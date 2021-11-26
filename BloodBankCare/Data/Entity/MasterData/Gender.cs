using BloodBankCare.Data.Entity.ApplicationUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Data.Entity.MasterData
{
    public class Gender:Base
    {
        public string genderName { get; set; }
        public virtual IEnumerable<ApplicationUser> ApplicationUsers { get; set; }
        


    }
}
