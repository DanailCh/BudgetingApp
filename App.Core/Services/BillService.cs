using App.Core.Contracts;
using App.Core.Enum;
using App.Core.Models.Archive.Bill;
using App.Core.Models.Bill;
using App.Core.Models.BillType;
using App.Infrastructure.Data.Models;
using HouseholdBudgetingApp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
        public async Task<ArchiveBillQueryModel> AllBillsAsync(
            string userId,
            DateTime? billMonth,
            int? billTypeId,            
            BillsSorting sortingDate = BillsSorting.None,
            BillsSorting sortingCost = BillsSorting.None,            
            int currentPage = 1,
            int billsPerPage=10)
        {
            var allBills = _context.Bills.AsNoTracking().Where(b => b.UserId == userId && b.DeletedOn == null && b.IsArchived == true);

            if (billMonth!=null)
            {
                allBills = allBills
                    .Where(b => b.Date==billMonth);
                sortingDate = BillsSorting.None;
            }

            if (billTypeId != 0)
            {
                allBills = allBills
                    .Where(b => (b.UserId == userId || b.UserId == null) && b.DeletedOn == null && b.BillTypeId == billTypeId);
            }

           

          
            switch (sortingDate)
            {
                case BillsSorting.DateAscending:
                    switch (sortingCost)
                    {
                        case BillsSorting.CostCheapest:
                            allBills=allBills
                             .OrderBy(b => b.Date)
                            .ThenBy(b => b.Cost);
                            break;
                        case BillsSorting.CostExpensive:
                            allBills = allBills
                           .OrderBy(b => b.Date)
                           .ThenByDescending(b => b.Cost);
                            break;
                        case BillsSorting.None:
                            allBills = allBills
                           .OrderBy(b => b.Date);                           
                            break;

                    };
                    break;
                case BillsSorting.DateDescending:
                    switch (sortingCost)
                    {
                        case BillsSorting.CostCheapest:
                            allBills = allBills
                             .OrderByDescending(b => b.Date)
                             .ThenBy(b => b.Cost);
                            break;
                        case BillsSorting.CostExpensive:
                            allBills = allBills
                           .OrderByDescending(b => b.Date)
                           .ThenByDescending(b => b.Cost);
                            break;
                        case BillsSorting.None:
                            allBills = allBills
                           .OrderByDescending(b => b.Date);
                            break;
                    };
                    break;
                case BillsSorting.None:
                    switch (sortingCost)
                    {
                        case BillsSorting.CostCheapest:
                            allBills = allBills
                             .OrderByDescending(b => b.Cost);
                            break;
                        case BillsSorting.CostExpensive:
                            allBills = allBills
                           .OrderByDescending(b => b.Cost);
                            break;
                        case BillsSorting.None:
                            allBills = allBills
                           .OrderByDescending(b => b.Id);
                            break;
                    };
                    break;

            };

            var billsToShow = await allBills
                .Skip((currentPage - 1) * billsPerPage)
                .Take(billsPerPage)
                .Select(b => new ArchiveBillViewModel()
                {
                    Id = b.Id,
                    Cost=b.Cost,
                    BillTypeName=b.BillType.Name,
                    Date=b.Date,
                })
                .ToListAsync();
            var billsForDownload = await allBills.Select(b => new ArchiveBillViewModel()
            {
                Id = b.Id,
                Cost = b.Cost,
                BillTypeName = b.BillType.Name,
                Date = b.Date,
            }).ToListAsync();
            int totalArchivedBills = await allBills.CountAsync();

            return new ArchiveBillQueryModel()
            {
                ArchivedBillsForDownload=billsForDownload,
                ArchivedBills = billsToShow,
                ArchivedBillsCount = totalArchivedBills,                
            };
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
            bool isPayed = false;
            if (model.PayerId != null) { isPayed = true;};
            var bill = new Bill
            {
                BillTypeId = model.BillTypeId,
                Cost = model.Cost, 
                IsPayed=isPayed,
                PayerId = model.PayerId,
                UserId = userId,
                Date = await GetDateAsync(userId)
            };
            await _context.Bills.AddAsync(bill);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBillByIdAsync(int id)
        {
            var bill = await _context.Bills.FindAsync(id);
            if (bill!=null)
            {
                bill.DeletedOn = GetDate();
                await _context.SaveChangesAsync();
            }
        }

        public async Task EditBillByIdAsync(BillFormModel model, int id)
        {
            var bill = await _context.Bills.FindAsync(id);
            if (bill!=null)
            {
                bill.BillTypeId = model.BillTypeId;
                bill.Cost = model.Cost;

                await _context.SaveChangesAsync();
            }  
        }

        public async Task<BillFormModel?> FindBillByIdAsync(int id)
        {
            BillFormModel foundBill=new BillFormModel();
           var bill=await _context.Bills.FindAsync(id);
            if (bill!=null)
            {
                foundBill = new BillFormModel()
                {
                    BillTypeId = bill.BillTypeId,
                    Cost = bill.Cost,

                };
            };
            return foundBill;
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

        public async Task<DateTime> GetDateAsync(string userId)
        {
            DateTime date = DateTime.Now;
            if (await _context.Bills.AnyAsync(b=>b.IsArchived==true&&b.UserId==userId))
            {
                date = await _context.Bills.Where(b => b.IsArchived == true && b.UserId == userId).OrderBy(b => b.Date).Select(b => b.Date).LastAsync();
                date=date.AddMonths(1);
            }
            return new DateTime(date.Year, date.Month, 1);            
        }

        public async Task<string> GetFormatedDateAsync(string userId)
        {
            var date = await GetDateAsync(userId);
            return date.ToString("MMMM yyyy");
        }

        public  DateTime GetDate()
        {
            DateTime date = DateTime.Now;
            return new DateTime(date.Year, date.Month, 1);
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
