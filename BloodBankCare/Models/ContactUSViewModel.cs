using BloodBankCare.Data.Entity.Home;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Models
{
    public class ContactUSViewModel
    {
        public string name { get; set; }
        public string email { get; set; }
        public string contactNo { get; set; }
        public string subject { get; set; }
        public string message { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? date { get; set; }
        public virtual ICollection<ContactUS> contactUsList { get; set; }
        public virtual ContactUS contactUs { get; set; }
    }
}
