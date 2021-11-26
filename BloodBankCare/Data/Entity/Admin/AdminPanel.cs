using BloodBankCare.Data.Entity.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Data.Entity.Admin
{
    public class AdminPanelInfo:Base
    {
        public string name { get; set; }
        public string userId { get; set; }
        public string profession { get; set; }
        public string instituteName { get; set; }
        public string instituteType { get; set; }  //business,govt,semi govt,pvt,multinational
        public string designation { get; set; }
        public string address { get; set; }
        public string ImageUrl { get; set; }
        public int? role { get; set; }  //1=admin,2=volunteer
        public string contactNo { get; set; }

    }
}
