using App.Core.Contracts;
using App.Core.Models.BudgetSummary;
using HouseholdBudgetingApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Services
{
    public class SummaryLogicService:ISummaryLogicService
    {
        private decimal householdIncome;
        private decimal householdExpences;
        private Dictionary<string, decimal> memberPayed=new Dictionary<string, decimal>();

        private readonly ApplicationDbContext _context;
        public SummaryLogicService(ApplicationDbContext context)
        {
            _context = context;
        }

        public string GetSummary(IEnumerable<SummaryFormModel> s,string userId)
        {
            throw new NotImplementedException();

            //return string.Empty;
        }

        public Task<decimal> GetHouseholdExpences(DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}
