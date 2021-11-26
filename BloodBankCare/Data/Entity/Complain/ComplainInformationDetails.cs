using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Data.Entity.Complain
{
    public class ComplainInformationDetails:Base
    {
        //user er complain form theke data asbe tarpor ei table er sathe sathe complain information e o data porbe

        public string complainBy { get; set; }  //Name 
        public string complainHeadLine { get; set; }
        public string complainDeatils { get; set; }
        public string email { get; set; }
        public string contact { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? comPlainDate { get; set; }
        public string Address { get; set; }

        public virtual ICollection<ComplainSolveInfoDetails> ComplainSolveInfoDetails { get; set; }
        public virtual ICollection<ComplainInformation> ComplainInformations { get; set; }
    }
}
