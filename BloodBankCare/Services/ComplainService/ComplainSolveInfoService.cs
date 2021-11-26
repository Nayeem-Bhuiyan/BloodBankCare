using BloodBankCare.Data;
using BloodBankCare.Data.Entity.Complain;
using BloodBankCare.Services.ComplainService.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Services.ComplainService
{
    public class ComplainSolveInfoService: IComplainSolveInfoService
	{
		public readonly AppDbContext _context;

		public ComplainSolveInfoService(AppDbContext context)
		{
			_context = context;
		}


		#region ComplainSolveInfo

		public async Task<IEnumerable<ComplainSolveInfo>> GetAllComplainSolveInfo()
		{
			return await _context.ComplainSolveInfos.OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
		}

		public async Task<ComplainSolveInfo> GetComplainSolveInfoById(int? id)
		{
			return await _context.ComplainSolveInfos.FindAsync(id);
		}



		public async Task<int> SaveComplainSolveInfo(ComplainSolveInfo model)
		{
			if (model.Id != 0)
				_context.ComplainSolveInfos.Update(model);
			else
				_context.ComplainSolveInfos.Add(model);

			return await _context.SaveChangesAsync();

		}

		public async Task<bool> DeleteComplainSolveInfoById(int? id)
		{
			_context.ComplainSolveInfos.Remove(_context.ComplainSolveInfos.Find(id));
			return 1 == await _context.SaveChangesAsync();
		}
        #endregion
    }
}
