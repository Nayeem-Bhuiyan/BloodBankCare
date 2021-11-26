using BloodBankCare.Data.Entity.Complain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Areas.Complain.Models
{
    public class ComplainInformationDetailsViewModel
    {
        //user er complain form theke data asbe tarpor ei table er sathe sathe complain information e o data porbe
        public int ComplainInformationDetailsId { get; set; }
        public string complainBy { get; set; }  //Name 
        public string complainHeadLine { get; set; }
        public string complainDeatils { get; set; }
        public string email { get; set; }
        public string contact { get; set; }
        public DateTime? comPlainDate { get; set; }
        public string Address { get; set; }

        public virtual ComplainInformationDetails complainInformationDetail { get; set; }
        public virtual IEnumerable<ComplainInformationDetails> complainInformationDetails { get; set; }
        public virtual IEnumerable<ComplainInformation> complainInformations { get; set; }
    }
}
