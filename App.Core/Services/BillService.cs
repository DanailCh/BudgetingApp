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
        private readonly IHouseholdService _householdService;
        public BillService(ApplicationDbContext context,IHouseholdService householdService)
        {
            _householdService = householdService;
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

        public async Task<IEnumerable<BillViewModel>> AllCurentMonthBillsAsync(string userId,DateTime date)
        {
            var bills = await _context.Bills.Where(b => b.UserId == userId && b.DeletedOn == null&&b.Date==date).Select(b => new BillViewModel
            {
                Id = b.Id,
                BillTypeName = b.BillType.Name,
                Cost = b.Cost,
                PayedBy = b.Payer.Name,
                IsPayed = b.IsPayed,
            }).ToListAsync();


            return bills;
        }

        public Task<bool> BillBelongsToUserAsync(int id, string userId)
        {
            return _context.Bills.AnyAsync(b => b.Id == id&& b.UserId==userId&&b.DeletedOn==null);
        }

        public async Task<bool> BillExistsAsync(int id)
        {
            return await _context.Bills.AnyAsync(b=>b.Id == id&&b.DeletedOn==null);
        }

        public async Task<bool> BillIsPayedAsync(int id)
        {
            return await _context.Bills.AnyAsync(b => b.Id == id&&b.IsPayed==true);
        }

        public async Task CreateBillAsync(BillFormModel model,string userId)
        {
         
            var bill = new Bill
            {
                BillTypeId = model.BillTypeId,
                Cost = model.Cost,
                IsPayed = model.IsPayed,
                PayerId = model.PayerId,
                UserId = userId,
                Date = GetDate()
            };
            await _context.Bills.AddAsync(bill);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBillByIdAsync(int id)
        {
            var bill = await _context.Bills.FindAsync(id);
            bill.DeletedOn = GetDate();
            await _context.SaveChangesAsync();
        }

        public async Task EditBillByIdAsync(BillFormModel model, int id)
        {
            var bill = await _context.Bills.FindAsync(id);
            bill.BillTypeId = model.BillTypeId;
            bill.Cost = model.Cost;
            
            await _context.SaveChangesAsync();


        }

        public async Task<BillFormModel> FindBillByIdAsync(int id)
        {
            BillFormModel foundBill;
           var bill=await _context.Bills.FindAsync(id);           

            return foundBill = new BillFormModel()
            { 
                BillTypeId = bill.BillTypeId,
                Cost=bill.Cost,
                               
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

        public DateTime GetDate()
        {
            DateTime date = DateTime.Now;
            return new DateTime(date.Year, date.Month, 1);            
        }

        public string GetFormatedDate()
        { 
            return GetDate().ToString("MMMM yyyy");
        }

        public async Task PayBillAsync(BillViewModel model, int id)
        {
            var bill = await _context.Bills.FindAsync(id);
            bill.IsPayed = true;
            bill.PayerId = model.PayerId;

            await _context.SaveChangesAsync();
        }
    }
}
