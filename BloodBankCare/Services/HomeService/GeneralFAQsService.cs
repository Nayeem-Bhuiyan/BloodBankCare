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
    public class GeneralFAQsService: IGeneralFAQsService
	{
		public readonly AppDbContext _context;

		public GeneralFAQsService(AppDbContext context)
		{
			_context = context;
		}


		#region GeneralFAQS

		public async Task<IEnumerable<GeneralFAQS>> GetAllGeneralFAQS()
		{
			return await _context.GeneralFAQs.OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
		}

		public async Task<GeneralFAQS> GetGeneralFAQSById(int? id)
		{
			return await _context.GeneralFAQs.FindAsync(id);
		}



		public async Task<int> SaveGeneralFAQS(GeneralFAQS model)
		{
			if (model.Id != 0)
				_context.GeneralFAQs.Update(model);
			else
				_context.GeneralFAQs.Add(model);

			return await _context.SaveChangesAsync();

		}

		public async Task<bool> DeleteGeneralFAQSById(int? id)
		{
			_context.GeneralFAQs.Remove(_context.GeneralFAQs.Find(id));
			return 1 == await _context.SaveChangesAsync();
		}
		#endregion
	}
}
