using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Data.Entity.Home
{
    public class ContactUS:Base
    {
        public string name { get; set; }
        public string email { get; set; }
        public string contactNo { get; set; }
        public string subject { get; set; }
        public string message { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? date { get; set; }
    }
}
