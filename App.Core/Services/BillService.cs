using App.Core.Contracts;
using App.Core.Models.Bill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseholdBudgetingApp.Data;
using Microsoft.EntityFrameworkCore;
using App.Infrastructure.Data.Models;
using App.Core.Models.BillType;

namespace App.Core.Services
{
    public class BillService : IBillService
    {
        private readonly ApplicationDbContext _context;
        public BillService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<BillViewModel>> AllBillsAsync(string userId)
        {
            var bills = await _context.Bills.AsNoTracking().Where(b => b.UserId == userId).Select(b => new BillViewModel
            {
                Id = b.Id,
                BillTypeName = b.BillType.Name,
                Cost = b.Cost,
                PayedBy = b.Payer.Name
            }).ToListAsync();
            return bills;
            
        }

        public async Task CreateBillAsync(BillFormModel model,string userId)
        {
            var bill = new Bill
            {
                BillTypeId = model.BillTypeId,
                Cost = model.Cost,
                PayerId = model.PayerId,
                UserId = userId,
                Date = model.Date,
            };
            await _context.Bills.AddAsync(bill);
            await _context.SaveChangesAsync();
        }

        public Task DeleteBillByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task EditBillByIdAsync(BillFormModel model, int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BillTypeViewModel>> GetBillTypesAsync(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
