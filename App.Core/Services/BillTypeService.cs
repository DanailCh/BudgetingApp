using App.Core.Contracts;
using App.Core.Models.Bill;
using App.Core.Models.BillType;
using App.Infrastructure.Data.Models;
using HouseholdBudgetingApp.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Services
{
    public class BillTypeService : IBillTypeService
    {
        private readonly ApplicationDbContext _context;
        public BillTypeService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<BillTypeFormViewModel>> AllCustomBillTypesAsync(string userId)
        {
            var billTypes = await _context.BillTypes.AsNoTracking().Where(b => b.UserId == userId && b.DeletedOn == null).Select(b => new BillTypeFormViewModel
            {
                Id = b.Id,
                Name = b.Name,
                
            }).ToListAsync();
            return billTypes;
        }

        public async Task<bool> BillTypeExistsAsync(BillTypeFormViewModel model, string userId)
        {
            return await _context.BillTypes.AsNoTracking().AnyAsync(bt => (bt.UserId == userId || bt.UserId == null) && bt.Name.ToLower() == model.Name.ToLower() && bt.DeletedOn == null);
        }

        public async Task CreateCustomBillTypeAsync(BillTypeFormViewModel model, string userId)
        {
            var billType = new BillType
            {
                Name=model.Name,
                UserId = userId,
                
            };
            await _context.BillTypes.AddAsync(billType);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCustomBillTypeByIdAsync(int id)
        {
            var billType = await _context.BillTypes.FindAsync(id);
            if (billType != null)
            {
                billType.DeletedOn = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

       
    }
}
