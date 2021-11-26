using BloodBankCare.Data;
using BloodBankCare.Data.Entity.NoticeBoard;
using BloodBankCare.Services.NoticeBoardService.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Services.NoticeBoardService
{
    public class NoticeBoardInfoService: INoticeBoardInfoService
	{
		public readonly AppDbContext _context;

		public NoticeBoardInfoService(AppDbContext context)
		{
			_context = context;
		}


		#region NoticeBoardInfo

		public async Task<IEnumerable<NoticeBoardInfo>> GetAllNoticeBoardInfo()
		{
			return await _context.NoticeBoardInfos.OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
		}

		public async Task<NoticeBoardInfo> GetNoticeBoardInfoById(int? id)
		{
			return await _context.NoticeBoardInfos.FindAsync(id);
		}



		public async Task<int> SaveNoticeBoardInfo(NoticeBoardInfo model)
		{
			if (model.Id != 0)
				_context.NoticeBoardInfos.Update(model);
			else
				_context.NoticeBoardInfos.Add(model);

			return await _context.SaveChangesAsync();

		}

		public async Task<bool> DeleteNoticeBoardInfoById(int? id)
		{
			_context.NoticeBoardInfos.Remove(_context.NoticeBoardInfos.Find(id));
			return 1 == await _context.SaveChangesAsync();
		}
		#endregion
	}
}
