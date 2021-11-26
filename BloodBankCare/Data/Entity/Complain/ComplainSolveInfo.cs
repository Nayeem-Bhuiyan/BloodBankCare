using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Data.Entity.Complain
{
    public class ComplainSolveInfo : Base
    {

        public int? ComplainSolveInfoDetailsId { get; set; }   //complain info List er solve button e ComplainInformationId  include kora thakbe then click korle modal e hiden field e jabe sathe oporer field gular information niye ei table insert hobe
        public virtual ComplainSolveInfoDetails ComplainSolveInfoDetails { get; set; }


        public int? isSolved { get; set; }  //0=pending,1=reject,2=solved
        public int? isApprovedByAdmin { get; set; } //0=pending(user),1=reject(admin),2=approved(admin)  //akta solve id jodi approve hoi sathe sathe oi id r baki record gulo reject hoye jabe 


    }
}
