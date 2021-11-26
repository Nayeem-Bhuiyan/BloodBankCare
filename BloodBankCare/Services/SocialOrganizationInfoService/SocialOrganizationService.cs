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
    public class SocialOrganizationService: ISocialOrganizationService
	{
		public readonly AppDbContext _context;

		public SocialOrganizationService(AppDbContext context)
		{
			_context = context;
		}


		#region SocialOrganization

		public async Task<IEnumerable<SocialOrganization>> GetAllSocialOrganization()
		{
			return await _context.SocialOrganizations.OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
		}

		public async Task<SocialOrganization> GetSocialOrganizationById(int? id)
		{
			return await _context.SocialOrganizations.FindAsync(id);
		}



		public async Task<int> SaveSocialOrganization(SocialOrganization model)
		{
			if (model.Id != 0)
				_context.SocialOrganizations.Update(model);
			else
				_context.SocialOrganizations.Add(model);

			return await _context.SaveChangesAsync();

		}

		public async Task<bool> DeleteSocialOrganizationById(int? id)
		{
			_context.SocialOrganizations.Remove(_context.SocialOrganizations.Find(id));
			return 1 == await _context.SaveChangesAsync();
		}
		#endregion
	}
}
