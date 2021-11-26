using BloodBankCare.Data.Entity.NoticeBoard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Services.NoticeBoardService.Interfaces
{
  public  interface INoticeBoardInfoService
    {
        Task<IEnumerable<NoticeBoardInfo>> GetAllNoticeBoardInfo();
        Task<NoticeBoardInfo> GetNoticeBoardInfoById(int? id);
        Task<int> SaveNoticeBoardInfo(NoticeBoardInfo model);
        Task<bool> DeleteNoticeBoardInfoById(int? id);
    }
}
