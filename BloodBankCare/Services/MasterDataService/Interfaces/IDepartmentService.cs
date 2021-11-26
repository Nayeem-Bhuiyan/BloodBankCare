using BloodBankCare.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Services.MasterDataService.Interfaces
{
   public interface IDepartmentService
    {
        Task<IEnumerable<Department>> GetAllDepartment();
        Task<Department> GetDepartmentById(int? id);
        Task<int> SaveDepartment(Department model);
        Task<bool> DeleteDepartmentById(int? id);
    }
}
