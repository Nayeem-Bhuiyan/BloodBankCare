using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Data.Entity.Complain
{
    public class ComplainSolveInfoDetails:Base
    {
        //ComplainInformationDetails List er solve button(ComplainInformationDetailsId) e click korle modal asbe abong ComplainInformationDetailsId soho ei table er data insert hobe
        //ak e sathe ComplainSolveInfo de data porbe

        public int? ComplainInformationDetailsId { get; set; }
        public virtual ComplainInformationDetails ComplainInformationDetails { get; set; }

        public string solvedBy { get; set; }  //UserId/email who solve it
        public string contact { get; set; }  //mobile/email who solve it
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? solveDate { get; set; }
        public string solveinfoHeadLine { get; set; }
        public string solveinfoDetails { get; set; }

        public virtual ICollection<ComplainSolveInfo> ComplainSolveInfos { get; set; }
    }
}
