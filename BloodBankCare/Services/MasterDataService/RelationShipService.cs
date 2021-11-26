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
    public class RelationShipService: IRelationShipService
    {
		public readonly AppDbContext _context;

		public RelationShipService(AppDbContext context)
		{
			_context = context;
		}


		#region RelationShip

		public async Task<IEnumerable<RelationShip>> GetAllRelationShip()
		{
			return await _context.RelationShips.OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
		}

		public async Task<RelationShip> GetRelationShipById(int? id)
		{
			return await _context.RelationShips.FindAsync(id);
		}



		public async Task<int> SaveRelationShip(RelationShip model)
		{
			if (model.Id != 0)
				_context.RelationShips.Update(model);
			else
				_context.RelationShips.Add(model);

			return await _context.SaveChangesAsync();

		}

		public async Task<bool> DeleteRelationShipById(int? id)
		{
			_context.RelationShips.Remove(_context.RelationShips.Find(id));
			return 1 == await _context.SaveChangesAsync();
		}
		#endregion
	}
}
