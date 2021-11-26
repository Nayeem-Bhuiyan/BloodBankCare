using BloodBankCare.Data.Entity.Complain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Services.ComplainService.Interfaces
{
   public interface IComplainSolveInfoDetailsService
    {
        Task<IEnumerable<ComplainSolveInfoDetails>> GetAllComplainSolveInfoDetails();
        Task<ComplainSolveInfoDetails> GetComplainSolveInfoDetailsById(int? id);
        Task<int> SaveComplainSolveInfoDetails(ComplainSolveInfoDetails model);
        Task<bool> DeleteComplainSolveInfoDetailsById(int? id);
        Task<ComplainSolveInfoDetails> GetComplainSolveInfoDetailsByComDetailsId(int? id);
    }
}
