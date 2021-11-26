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
    public class ProfessionService: IProfessionService
	{
		public readonly AppDbContext _context;

		public ProfessionService(AppDbContext context)
		{
			_context = context;
		}


		#region Profession

		public async Task<IEnumerable<Profession>> GetAllProfession()
		{
			return await _context.Professions.OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
		}

		public async Task<Profession> GetProfessionById(int? id)
		{
			return await _context.Professions.FindAsync(id);
		}



		public async Task<int> SaveProfession(Profession model)
		{
			if (model.Id != 0)
				_context.Professions.Update(model);
			else
				_context.Professions.Add(model);

			return await _context.SaveChangesAsync();

		}

		public async Task<bool> DeleteProfessionById(int? id)
		{
			_context.Professions.Remove(_context.Professions.Find(id));
			return 1 == await _context.SaveChangesAsync();
		}
		#endregion
	}
}
