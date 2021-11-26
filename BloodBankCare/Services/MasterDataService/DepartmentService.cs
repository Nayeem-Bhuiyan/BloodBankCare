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
    public class DepartmentService: IDepartmentService
	{
		public readonly AppDbContext _context;

		public DepartmentService(AppDbContext context)
		{
			_context = context;
		}


		#region Department

		public async Task<IEnumerable<Department>> GetAllDepartment()
		{
			return await _context.Departments.OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
		}

		public async Task<Department> GetDepartmentById(int? id)
		{
			return await _context.Departments.FindAsync(id);
		}



		public async Task<int> SaveDepartment(Department model)
		{
			if (model.Id != 0)
				_context.Departments.Update(model);
			else
				_context.Departments.Add(model);

			return await _context.SaveChangesAsync();

		}

		public async Task<bool> DeleteDepartmentById(int? id)
		{
			_context.Departments.Remove(_context.Departments.Find(id));
			return 1 == await _context.SaveChangesAsync();
		}
		#endregion
	}
}
