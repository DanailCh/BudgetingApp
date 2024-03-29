using App.Core.Contracts;
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

        public async Task CreateSummary(List<MemberSalaryFormModel> model, string userId)
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



        public async Task<List<MemberSalaryFormModel>> GetMemberSalaryFormModelsAsync(string userId)
        {
            var members = await _context.HouseholdMembers.Where(m => m.UserId == userId && m.DeletedOn == null).Select(m => new MemberSalaryFormModel()
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
    }
}
