using App.Core.Contracts;
using App.Core.Enum;
using App.Core.Models.Archive.Bill;
using App.Core.Models.Archive.HouseholdBudget;
using App.Core.Models.BudgetSummary;
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
    public class BudgetSummaryService : IBudgetSummaryService
    {
        private readonly ApplicationDbContext _context;
        private readonly ISummaryLogicService _summaryLogicService;
        private readonly IBillService billService;
        public BudgetSummaryService(ApplicationDbContext context, ISummaryLogicService summaryLogicService, IBillService billService)
        {
            _context = context;
            _summaryLogicService = summaryLogicService;
            this.billService = billService;
        }

        public async Task<bool> NotAllBillsPayedAsync(string userId)
        {
            return await _context.Bills.AnyAsync(b=>b.UserId==userId&&b.IsPayed==false&&b.DeletedOn==null);
        }

        public async Task<IEnumerable<SummaryViewModel>> AllSummariesAsync(string userId)
        {
            var summaries = await _context.EndMonthSummaries.AsNoTracking().Where(x => x.UserId == userId).Select(x => new SummaryViewModel()
            {
                Id = x.Id,
                Date = x.Date.ToString("MMMM yyyy"),
                Summary = x.Summary,
                IsResolved = x.IsResolved,

            }).ToListAsync();
            return summaries;
        }

        public async Task CreateSummary(List<MemberSalaryFormViewModel> model, string userId)
        {
            var date=await billService.GetDateAsync(userId);
            string summary = await _summaryLogicService.GetSummary(model, userId,date);
            var newSummary = new EndMonthSummary
            {
                Summary = summary,
                IsResolved = false,
                UserId = userId,
                Date = date,
            };
            await _summaryLogicService.ArchiveBills(userId);

            await _context.EndMonthSummaries.AddAsync(newSummary);
            await _context.SaveChangesAsync();
        }

        public async Task<List<MemberSalaryFormViewModel>> GetMemberSalaryFormModelsAsync(string userId)
        {
            var members = await _context.HouseholdMembers.Where(m => m.UserId == userId && m.DeletedOn == null).Select(m => new MemberSalaryFormViewModel()
            {
                Id = m.Id,
                Name = m.Name,

            }).ToListAsync();
            return members;
        }

        public async Task ResolveSummary(int id)
        {
            var summary = await _context.EndMonthSummaries.FindAsync(id);
            summary.IsResolved = true;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> SummaryExistsAsync(int id)
        {
            return await _context.EndMonthSummaries.AnyAsync(m => m.Id == id);
        }

        public async Task<bool> SummaryBelongsToUserAsync(int id, string userId)
        {
            return await _context.EndMonthSummaries.AnyAsync(m=>m.Id==id&&m.UserId==userId);
        }

        public async Task<ArchiveHouseholdBudgetQueryModel> AllBudgetsAsync(string userId,AllArchivedBudgetsQueryModel model)
        {
            var budgetsToShow = _context.HouseholdBudgets.AsNoTracking().Where(b => b.UserId == userId);

            if (model.BudgetMonth != null)
            {
                budgetsToShow = budgetsToShow
                    .Where(b => b.Date == model.BudgetMonth);
                model.Sorting = BudgetSorting.None;
            }


            switch (model.Sorting)
            {
                case BudgetSorting.MostIncome:
                    budgetsToShow = budgetsToShow
                              .OrderByDescending(b => b.Income);
                    break;
                case BudgetSorting.LeastIncome:
                    budgetsToShow = budgetsToShow
                              .OrderBy(b => b.Income);
                    break;
                case BudgetSorting.MostExpences:
                    budgetsToShow = budgetsToShow
                              .OrderByDescending(b => b.Expences);
                    break;
                case BudgetSorting.LeastExpences:
                    budgetsToShow = budgetsToShow
                              .OrderBy(b => b.Expences);
                    break;
                case BudgetSorting.None:
                    budgetsToShow = budgetsToShow
                              .OrderByDescending(b => b.Id);
                    break;


            };

            var budgets = await budgetsToShow
                .Skip((model.CurrentPage - 1) * model.BudgetsPerPage)
                .Take(model.BudgetsPerPage)
                .Select(b => new ArchiveHouseholdBudgetViewModel()
                {
                    Id = b.Id,
                    Income = b.Income,
                    Expences = b.Expences,
                    Date = b.Date,
                })
                .ToListAsync();

            int totalArchivedBudgets = await budgetsToShow.CountAsync();

            return new ArchiveHouseholdBudgetQueryModel()
            {
               ArchivedBudgets=budgets,
               ArchivedBudgetsCount=totalArchivedBudgets,
            };
        }
    }
}
