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
    public class ThanaService: IThanaService
	{
		public readonly AppDbContext _context;

		public ThanaService(AppDbContext context)
		{
			_context = context;
		}


		#region Thana
		public async Task<IEnumerable<Thana>> GetThanasByDistrictId(int? id)
		{
			return await _context.Thanas.Where(x=>x.DistrictId==id).AsNoTracking().ToListAsync();
		}

		public async Task<IEnumerable<Thana>> GetAllThana()
		{
			return await _context.Thanas.Include(d=>d.District).OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
		}

		public async Task<Thana> GetThanaById(int? id)
		{
			return await _context.Thanas.FindAsync(id);
		}



		public async Task<int> SaveThana(Thana model)
		{
			if (model.Id != 0)
				_context.Thanas.Update(model);
			else
				_context.Thanas.Add(model);

			return await _context.SaveChangesAsync();

		}

		public async Task<bool> DeleteThanaById(int? id)
		{
			_context.Thanas.Remove(_context.Thanas.Find(id));
			return 1 == await _context.SaveChangesAsync();
		}
		#endregion
	}
}
