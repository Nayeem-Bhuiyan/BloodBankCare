using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloodBankCare.Data;
using BloodBankCare.Data.Entity.Admin;
using BloodBankCare.Areas.Admin.Models;
using Microsoft.EntityFrameworkCore;
using BloodBankCare.Services.AdminPanelServices.Interfaces;

namespace BloodBankCare.Services.AdminPanelServices
{
  

    
    public class AdminPanelService: IAdminPanelService
	{

        public readonly AppDbContext _context;

        public AdminPanelService(AppDbContext context)
        {
            _context = context;
        }


		#region AdminPanelInfo

		public async Task<IEnumerable<AdminPanelInfo>> GetAllAdminPanelInfo()
		{
			return await _context.AdminPanelInfos.OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
		}

		public async Task<AdminPanelInfo> GetAdminPanelInfoById(int? id)
		{
			return await _context.AdminPanelInfos.FindAsync(id);
		}



		public async Task<int> SaveAdminPanelInfo(AdminPanelInfo model)
		{
			if (model.Id != 0)
				_context.AdminPanelInfos.Update(model);
			else
				_context.AdminPanelInfos.Add(model);

			return await _context.SaveChangesAsync();
			 
		}

		public async Task<bool> DeleteAdminPanelInfoById(int? id)
		{
			_context.AdminPanelInfos.Remove(_context.AdminPanelInfos.Find(id));
			return 1 == await _context.SaveChangesAsync();
		}



      #endregion




	}
}
