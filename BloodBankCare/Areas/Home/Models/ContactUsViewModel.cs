using BloodBankCare.Data.Entity.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Areas.Home.Models
{
    public class ContactUsViewModel
    {
        public int ContactUsId { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string contactNo { get; set; }
        public string message { get; set; }
        public DateTime? date { get; set; }

        public virtual ContactUS contactUS { get; set; }
        public virtual IEnumerable<ContactUS> contactUSList { get; set; }
    }
}
