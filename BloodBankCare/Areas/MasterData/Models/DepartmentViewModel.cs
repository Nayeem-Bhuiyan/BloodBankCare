using BloodBankCare.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Areas.MasterData.Models
{
    public class DepartmentViewModel
    {

        public int DepartmentId { get; set; }
        public string departmentName { get; set; }

        public virtual Department department { get; set; }
        public virtual IEnumerable<Department> departments { get; set; }
    }
}
