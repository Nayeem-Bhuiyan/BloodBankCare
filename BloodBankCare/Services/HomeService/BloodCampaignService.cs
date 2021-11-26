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
    public class BloodCampaignService: IBloodCampaignService
	{
		public readonly AppDbContext _context;

		public BloodCampaignService(AppDbContext context)
		{
			_context = context;
		}


		#region BloodCampaign

		public async Task<IEnumerable<BloodCampaign>> GetAllBloodCampaign()
		{
			return await _context.BloodCampaigns.OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
		}



		public async Task<BloodCampaign> GetBloodCampaignById(int? id)
		{
			return await _context.BloodCampaigns.FindAsync(id);
		}



		public async Task<int> SaveBloodCampaign(BloodCampaign model)
		{
			if (model.Id != 0)
				_context.BloodCampaigns.Update(model);
			else
				_context.BloodCampaigns.Add(model);

			return await _context.SaveChangesAsync();

		}

		public async Task<bool> DeleteBloodCampaignById(int? id)
		{
			_context.BloodCampaigns.Remove(_context.BloodCampaigns.Find(id));
			return 1 == await _context.SaveChangesAsync();
		}
        #endregion
    }
}
