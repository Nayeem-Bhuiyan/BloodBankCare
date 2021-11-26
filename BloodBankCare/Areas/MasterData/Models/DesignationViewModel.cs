using BloodBankCare.Data.Entity;
using BloodBankCare.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Areas.MasterData.Models
{
    public class DesignationViewModel
    {
        public int DesignationId { get; set; }
        public string designationName { get; set; }


        public virtual Designation designation { get; set; }
        public virtual IEnumerable<Designation> designations { get; set; }
    }
}
