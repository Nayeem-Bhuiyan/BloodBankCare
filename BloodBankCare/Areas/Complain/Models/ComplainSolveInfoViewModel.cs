using BloodBankCare.Data.Entity.Complain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Areas.Complain.Models
{
    public class ComplainSolveInfoViewModel
    {
        public int ComplainSolveInfoId { get; set; }
        //fk
        public int? ComplainSolveInfoDetailsId { get; set; }   //complain info List er solve button e ComplainInformationId  include kora thakbe then click korle modal e hiden field e jabe sathe oporer field gular information niye ei table insert hobe

        public int? isSolved { get; set; }  //0=pending,1=reject,2=solved
        public int? isApprovedByAdmin { get; set; } //0=pending(user),1=reject(admin),2=approved(admin)  //akta solve id jodi approve hoi sathe sathe oi id r baki record gulo reject hoye jabe 

        public virtual ComplainSolveInfo complainSolveInfo { get; set; }
        public virtual IEnumerable<ComplainSolveInfo> complainSolveInfos { get; set; }
        public virtual IEnumerable<ComplainSolveInfoDetails> ComplainSolveInfoDetails { get; set; }
    }
}
