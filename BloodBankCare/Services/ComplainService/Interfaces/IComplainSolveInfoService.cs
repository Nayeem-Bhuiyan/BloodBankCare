using BloodBankCare.Data.Entity.Complain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Services.ComplainService.Interfaces
{
   public interface IComplainSolveInfoService
    {
        Task<IEnumerable<ComplainSolveInfo>> GetAllComplainSolveInfo();
        Task<ComplainSolveInfo> GetComplainSolveInfoById(int? id);
        Task<int> SaveComplainSolveInfo(ComplainSolveInfo model);
        Task<bool> DeleteComplainSolveInfoById(int? id);
    }
}
