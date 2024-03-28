using App.Core.Contracts;
using App.Core.Models.BudgetSummary;
using HouseholdBudgetingApp.Data;
using Microsoft.Extensions.Configuration.UserSecrets;
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
        private  string userId;

        private readonly ApplicationDbContext _context;
        public SummaryLogicService(ApplicationDbContext context)
        {
            _context = context;
        }

        public string GetSummary(IEnumerable<SummaryFormModel> s,string _userId)
        {
            //userId = _userId;
            //householdExpences=GetHouseholdExpences()

            return string.Empty;
        }

        public  decimal GetHouseholdExpences(DateTime date)
        {
            return householdExpences = _context.Bills.Where(b => b.UserId == userId && b.DeletedOn == null && b.Date == date).Select(b=>b.Cost).Sum();
        }
    }
}
