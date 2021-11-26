using BloodBankCare.Data.Entity.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Services.HomeService.Interfaces
{
   public interface IDailyNewsUpdateService
    {
        Task<IEnumerable<DailyNewsUpdate>> GetAllDailyNewsUpdate();
        Task<DailyNewsUpdate> GetDailyNewsUpdateById(int? id);
        Task<int> SaveDailyNewsUpdate(DailyNewsUpdate model);
        Task<bool> DeleteDailyNewsUpdateById(int? id);
    }
}
