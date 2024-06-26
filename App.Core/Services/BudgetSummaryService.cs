﻿using App.Core.Contracts;
using App.Core.Enum;
using App.Core.Models.Archive.HouseholdBudget;
using App.Core.Models.BudgetSummary;
using App.Infrastructure.Data.Models;
using HouseholdBudgetingApp.Data;
using Microsoft.EntityFrameworkCore;

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
            string summary =  await _summaryLogicService.GetSummaryAsync(model, userId,date);
            var newSummary = new EndMonthSummary
            {
                Summary = summary,
                IsResolved = false,
                UserId = userId,
                Date = date,
            };
            await _summaryLogicService.ArchiveBillsAsync(userId);

            await _context.EndMonthSummaries.AddAsync(newSummary);
            await _context.SaveChangesAsync();
        }

        public async Task<List<MemberSalaryFormViewModel>> GetMemberSalaryFormModelsAsync(string userId)
        {
            var allParticipatinMembers=_context.Bills.Where(b=>b.UserId==userId&&b.IsArchived==false&&b.DeletedOn==null).Select(b=>b.PayerId).Distinct().ToList();
            var members = await _context.HouseholdMembers.Where(m => m.UserId == userId && allParticipatinMembers.Contains(m.Id)).Select(m => new MemberSalaryFormViewModel()
            {
                Id = m.Id,
                Name = m.Name,

            }).ToListAsync();
            return members;
        }

        public async Task ResolveSummary(int id)
        {
            var summary = await _context.EndMonthSummaries.FindAsync(id);
            if (summary!=null)
            {
                summary.IsResolved = true;
                await _context.SaveChangesAsync();
            }           
        }
       
        public async Task<ArchiveHouseholdBudgetQueryModel> AllBudgetsAsync(string userId,AllArchivedBudgetsQueryModel model)
        {
            var allBudgets = _context.HouseholdBudgets.AsNoTracking().Where(b => b.UserId == userId);

            if (model.BudgetMonth != null)
            {
                allBudgets = allBudgets
                    .Where(b => b.Date == model.BudgetMonth);
                model.Sorting = BudgetSorting.None;
            }


            switch (model.Sorting)
            {
                case BudgetSorting.MostIncome:
                    allBudgets = allBudgets
                              .OrderByDescending(b => b.Income);
                    break;
                case BudgetSorting.LeastIncome:
                    allBudgets = allBudgets
                              .OrderBy(b => b.Income);
                    break;
                case BudgetSorting.MostExpences:
                    allBudgets = allBudgets
                              .OrderByDescending(b => b.Expences);
                    break;
                case BudgetSorting.LeastExpences:
                    allBudgets = allBudgets
                              .OrderBy(b => b.Expences);
                    break;
                case BudgetSorting.None:
                    allBudgets = allBudgets
                              .OrderByDescending(b => b.Id);
                    break;


            };

            var budgetsToShow = await allBudgets
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
            var budgetsToDownload= await allBudgets                
                .Select(b => new ArchiveHouseholdBudgetViewModel()
                {
                    Id = b.Id,
                    Income = b.Income,
                    Expences = b.Expences,
                    Date = b.Date,
                })
                .ToListAsync();
            int totalArchivedBudgets = await allBudgets.CountAsync();

            return new ArchiveHouseholdBudgetQueryModel()
            {
                ArchivedBudgetsToDownload = budgetsToDownload,
               ArchivedBudgets=budgetsToShow,
               ArchivedBudgetsCount=totalArchivedBudgets,
            };
        }

        public async Task<bool> HasBillsAsync(string userId)
        {
            var date=await billService.GetDateAsync(userId);
            var bills=(await billService.AllCurentMonthBillsAsync(userId, date)).Count();
            if (bills==0)
            {
                return false;
            }
            return true;
        }

        public bool HouseholdIncomeIsZero(List<MemberSalaryFormViewModel> model)
        {
            bool isZero = true;
            decimal expences = 0;
            foreach (var item in model)
            {
                expences += item.Salary;
            }
            if (expences!=0)
            {
                isZero = false;
            }
            return isZero;
        }
    }
}
