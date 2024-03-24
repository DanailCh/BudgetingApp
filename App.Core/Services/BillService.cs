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
using Microsoft.Identity.Client;

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
            var bills = await _context.Bills.AsNoTracking().Where(b => b.UserId == userId&&b.DeletedOn==null).Select(b => new BillViewModel
            {
                Id = b.Id,
                BillTypeName = b.BillType.Name,
                Cost = b.Cost,
                PayedBy = b.Payer.Name ?? "Not Payed"
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

        public async Task DeleteBillByIdAsync(int id)
        {
            var bill = await _context.Bills.FindAsync(id);
            if (bill != null)
            {
                bill.DeletedOn = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task EditBillByIdAsync(BillFormModel model, int id)
        {
            var bill = await _context.Bills.FindAsync(id);
            if(bill != null)
            {
                bill.BillTypeId = model.BillTypeId;
                bill.Cost = model.Cost;
                bill.PayerId = model.PayerId;                
                await _context.SaveChangesAsync();
            }

            
        }

        public async Task<BillFormModel> FindBillByIdAsync(int id)
        {
            BillFormModel foundBill;
           var bill=await _context.Bills.FindAsync(id);
            if (bill==null)
            {
                return null;
            }

            return foundBill = new BillFormModel()
            { 
                BillTypeId = bill.BillTypeId,
                Cost=bill.Cost,
                PayerId=bill.PayerId,                
            };
        }

        public async Task<IEnumerable<BillTypeFormViewModel>> GetBillTypesAsync(string userId)
        {
           
             var types = await _context
                 .BillTypes.AsNoTracking()
                 .Where(b=>(b.UserId==userId || b.UserId == null)&&b.DeletedOn==null)
                 .Select(t => new BillTypeFormViewModel()
                 {
                     Id = t.Id,
                     Name = t.Name
                 }).ToListAsync();
             return types;
           
        }
    }
}
