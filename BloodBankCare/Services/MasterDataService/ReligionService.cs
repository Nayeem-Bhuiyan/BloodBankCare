using BloodBankCare.Data;
using BloodBankCare.Data.Entity.MasterData;
using BloodBankCare.Services.MasterDataService.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Services.MasterDataService
{
    public class ReligionService: IReligionService
	{
		public readonly AppDbContext _context;

		public ReligionService(AppDbContext context)
		{
			_context = context;
		}


		#region Religion

		public async Task<IEnumerable<Religion>> GetAllReligion()
		{
			return await _context.Religions.OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
		}

		public async Task<Religion> GetReligionById(int? id)
		{
			return await _context.Religions.FindAsync(id);
		}



		public async Task<int> SaveReligion(Religion model)
		{
			if (model.Id != 0)
				_context.Religions.Update(model);
			else
				_context.Religions.Add(model);

			return await _context.SaveChangesAsync();

		}

		public async Task<bool> DeleteReligionById(int? id)
		{
			_context.Religions.Remove(_context.Religions.Find(id));
			return 1 == await _context.SaveChangesAsync();
		}
		#endregion
	}
}
