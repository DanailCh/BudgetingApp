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
        public BudgetSummaryService(ApplicationDbContext context, ISummaryLogicService summaryLogicService)
        {
            _context = context;
            _summaryLogicService = summaryLogicService;
        }

        public async Task<IEnumerable<SummaryFormModel>> AllHouseholdMembersAsync(string userId)
        {
            var members = await _context.HouseholdMembers.Where(m => m.UserId == userId && m.DeletedOn == null).Select(m => new SummaryFormModel()
            {
                Id = m.Id,
                Name = m.Name,               

            }).ToListAsync();
            return members;
        }

        public async Task CreateSummary(IEnumerable<SummaryFormModel> summaryFormModels, string userId)
        {
            string summary = _summaryLogicService.GetSummary(summaryFormModels, userId);
            var newSummary = new EndMonthSummary
            {
                Summary=summary,
                IsResolved=false,
                UserId = userId,
                Date = DateTime.Now,
            };

            await _context.EndMonthSummaries.AddAsync(newSummary);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<SummaryViewModel>> GetAllEndMontSummariesAsync(string userId)
        {
            var summaries=await _context.EndMonthSummaries.AsNoTracking().Where(x => x.UserId == userId).Select(x=>new SummaryViewModel()
            {
                Id = x.Id,
                Date = x.Date,
                Summary = x.Summary,
                IsResolved=x.IsResolved,

            }).ToListAsync();
            return summaries;
        }

        public async Task ResolveSummarie(int id)
        {
            var summary = await _context.EndMonthSummaries.FindAsync(id);
            if (summary != null)
            {
                summary.IsResolved = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
