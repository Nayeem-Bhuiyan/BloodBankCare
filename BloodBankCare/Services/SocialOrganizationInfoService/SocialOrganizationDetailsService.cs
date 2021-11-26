using BloodBankCare.Data;
using BloodBankCare.Data.Entity.SocialOrganizationInfo;
using BloodBankCare.Services.SocialOrganizationInfoService.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Services.SocialOrganizationInfoService
{
    public class SocialOrganizationDetailsService: ISocialOrganizationDetailsService
	{
		public readonly AppDbContext _context;

		public SocialOrganizationDetailsService(AppDbContext context)
		{
			_context = context;
		}


		#region SocialOrganizationDetails

		public async Task<IEnumerable<SocialOrganizationDetails>> GetAllSocialOrganizationDetails()
		{
			return await _context.SocialOrganizationDetails.Include(x=>x.SocialOrganization).OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
		}

		public async Task<SocialOrganizationDetails> GetSocialOrganizationDetailsById(int? id)
		{
			return await _context.SocialOrganizationDetails.FindAsync(id);
		}



		public async Task<int> SaveSocialOrganizationDetails(SocialOrganizationDetails model)
		{
			if (model.Id != 0)
				_context.SocialOrganizationDetails.Update(model);
			else
				_context.SocialOrganizationDetails.Add(model);

			return await _context.SaveChangesAsync();

		}

		public async Task<bool> DeleteSocialOrganizationDetailsById(int? id)
		{
			_context.SocialOrganizationDetails.Remove(_context.SocialOrganizationDetails.Find(id));
			return 1 == await _context.SaveChangesAsync();
		}
		#endregion
	}
}
