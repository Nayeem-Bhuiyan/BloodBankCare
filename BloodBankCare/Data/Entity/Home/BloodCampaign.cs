using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Data.Entity.Home
{
    public class BloodCampaign:Base
    {

        public string chiefGuest { get; set; }
        public string specialGuest { get; set; }
        public string headingText { get; set; }
        public string deatilsDescription { get; set; }
        public string location { get; set; }
        public string occasion { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? date { get; set; }
        public string imageUrl { get; set; }

    }
}
