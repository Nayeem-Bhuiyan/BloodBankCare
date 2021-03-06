using BloodBankCare.Data.Entity.Complain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Areas.Complain.Models
{
    public class ComplainInformationViewModel
    {
        //complain info table er ak e sathe ei table data dokbe
        public int ComplainInformationId { get; set; }
        //fk
        public int? ComplainInformationDetailsId { get; set; }


        public int? isSolved { get; set; } //0=pending,1=reject,2=solved  (ComplainInformationDetails er List (reject,solve) button thakbe jar bitore ComplainInformationDetailsId included thakbe)
                                           //solve button click hole modal asbe solve details information niye  save hobe solve info table e

        public int? isApprovedByAdmin { get; set; } //0=pending(user),1=reject(admin),2=approved(admin)

        public virtual ComplainInformation complainInformation { get; set; }
        public virtual IEnumerable<ComplainInformation> complainInformations { get; set; }
        public virtual IEnumerable<ComplainInformationDetails> complainInformationDetails { get; set; }
        
    }
}
