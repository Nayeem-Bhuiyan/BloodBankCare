using BloodBankCare.Data.Entity.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Services.HomeService.Interfaces
{
   public interface IWellWisherMessageService
    {
        Task<IEnumerable<WellWisherMessage>> GetAllWellWisherMessage();
        Task<WellWisherMessage> GetWellWisherMessageById(int? id);
        Task<int> SaveWellWisherMessage(WellWisherMessage model);
        Task<bool> DeleteWellWisherMessageById(int? id);
    }
}
