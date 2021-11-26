using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Data.Entity.Home
{
    public class DailyNewsUpdate:Base
    {
        public string headingText { get; set; }
        public string detailsDescription { get; set; }
        public string location { get; set; }
        public string reason { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? date { get; set; }
        public string fileUrl { get; set; }

    }
}
