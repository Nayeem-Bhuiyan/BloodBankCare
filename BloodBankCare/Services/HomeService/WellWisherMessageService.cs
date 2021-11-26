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
    public class WellWisherMessageService: IWellWisherMessageService
	{
		public readonly AppDbContext _context;

		public WellWisherMessageService(AppDbContext context)
		{
			_context = context;
		}


		#region WellWisherMessage

		public async Task<IEnumerable<WellWisherMessage>> GetAllWellWisherMessage()
		{
			return await _context.WellWisherMessages.OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
		}

		public async Task<WellWisherMessage> GetWellWisherMessageById(int? id)
		{
			return await _context.WellWisherMessages.FindAsync(id);
		}



		public async Task<int> SaveWellWisherMessage(WellWisherMessage model)
		{
			if (model.Id != 0)
				_context.WellWisherMessages.Update(model);
			else
				_context.WellWisherMessages.Add(model);

			return await _context.SaveChangesAsync();

		}

		public async Task<bool> DeleteWellWisherMessageById(int? id)
		{
			_context.WellWisherMessages.Remove(_context.WellWisherMessages.Find(id));
			return 1 == await _context.SaveChangesAsync();
		}
		#endregion
	}
}
