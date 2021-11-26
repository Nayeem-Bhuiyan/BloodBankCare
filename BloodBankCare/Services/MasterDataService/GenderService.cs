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
    public class GenderService: IGenderService
	{
		public readonly AppDbContext _context;

		public GenderService(AppDbContext context)
		{
			_context = context;
		}
		

		#region Gender

		public async Task<IEnumerable<Gender>> GetAllGender()
		{
			return await _context.Genders.OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
		}

		public async Task<Gender> GetGenderById(int? id)
		{
			return await _context.Genders.FindAsync(id);
		}



		public async Task<int> SaveGender(Gender model)
		{
			if (model.Id != 0)
				_context.Genders.Update(model);
			else
				_context.Genders.Add(model);

			return await _context.SaveChangesAsync();

		}

		public async Task<bool> DeleteGenderById(int? id)
		{
			_context.Genders.Remove(_context.Genders.Find(id));
			return 1 == await _context.SaveChangesAsync();
		}
		#endregion
	}
}
