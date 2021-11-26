using BloodBankCare.Data.Entity.Complain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Areas.Complain.Models
{
    public class ComplainSolveInfoDetailsViewModel
    {
        //ComplainInformationDetails List er solve button(ComplainInformationDetailsId) e click korle modal asbe abong ComplainInformationDetailsId soho ei table er data insert hobe
        //ak e sathe ComplainSolveInfo de data porbe
        public int ComplainSolveInfoDetailsId { get; set; }
        //fk
        public int? ComplainInformationDetailsId { get; set; }
        public virtual IEnumerable<ComplainInformationDetails> ComplainInformationDetails { get; set; }
        

        public string solvedBy { get; set; }  //UserId/email who solve it
        public string contact { get; set; }  //mobile/email who solve it
        public DateTime? solveDate { get; set; }
        public string solveinfoHeadLine { get; set; }
        public string solveinfoDetails { get; set; }

        public virtual ComplainSolveInfoDetails complainSolveInfoDetail { get; set; }
        public virtual IEnumerable<ComplainSolveInfoDetails> complainSolveInfoDetails { get; set; }

    }
}
