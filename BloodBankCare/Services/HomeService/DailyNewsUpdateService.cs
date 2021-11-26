using BloodBankCare.Data;
using BloodBankCare.Data.Entity.Home;
using BloodBankCare.Services.HomeService.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Services.HomeService
{
    public class DailyNewsUpdateService: IDailyNewsUpdateService
	{
		public readonly AppDbContext _context;

		public DailyNewsUpdateService(AppDbContext context)
		{
			_context = context;
		}


		#region DailyNewsUpdate

		public async Task<IEnumerable<DailyNewsUpdate>> GetAllDailyNewsUpdate()
		{
			return await _context.DailyNewsUpdates.OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
		}

		public async Task<DailyNewsUpdate> GetDailyNewsUpdateById(int? id)
		{
			return await _context.DailyNewsUpdates.FindAsync(id);
		}



		public async Task<int> SaveDailyNewsUpdate(DailyNewsUpdate model)
		{
			if (model.Id != 0)
				_context.DailyNewsUpdates.Update(model);
			else
				_context.DailyNewsUpdates.Add(model);

			return await _context.SaveChangesAsync();

		}

		public async Task<bool> DeleteDailyNewsUpdateById(int? id)
		{
			_context.DailyNewsUpdates.Remove(_context.DailyNewsUpdates.Find(id));
			return 1 == await _context.SaveChangesAsync();
		}
		#endregion
	}
}
