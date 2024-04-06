using App.Core.Contracts;
using App.Core.Models.BudgetSummary;
using App.Infrastructure.Data.Models;
using HouseholdBudgetingApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace App.Core.Services
{
    public class SummaryLogicService:ISummaryLogicService
    {
        private decimal householdIncome;
        private decimal householdExpences;
        private Dictionary<int, decimal> memberPayed=new Dictionary<int, decimal>();
        private Dictionary<int, decimal> memberShouldHavePayed = new Dictionary<int, decimal>();
        private Dictionary<int, decimal> memberDifferance = new Dictionary<int, decimal>();

        private readonly ApplicationDbContext _context;
        public SummaryLogicService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> GetSummary(List<MemberSalaryFormViewModel> model,string _userId,DateTime date)
        {
            GetHouseholdExpences(_userId);
            GetHouseholdIncome(model);
            FillMembersPayed(model,_userId);
            FillMembersShouldHavePayed(model);
            FillMemberDifferance();

            string summary = Summarize(model,_userId);
            await  AddDataToDatabase(_userId, date,model);

            return summary;
        }

        private async Task AddDataToDatabase(string userId, DateTime date,List<MemberSalaryFormViewModel> members)
        {
            HouseholdBudget householdBudget = new HouseholdBudget()
            {
                Date = date,
                Income=householdIncome,
                Expences=householdExpences,
                UserId=userId,
            };
            await _context.HouseholdBudgets.AddAsync(householdBudget);
            List<MemberSalary> salaries = new List<MemberSalary>();
            foreach (var member in members)
            {
                salaries.Add(new MemberSalary()
                {
                    HouseholdMemberId=member.Id,
                    Date=date,
                    Salary=member.Salary,
                    UserId=userId,
                });
            };
            await _context.MemberSalaries.AddRangeAsync(salaries);
            await _context.SaveChangesAsync();
        }

        private string Summarize(List<MemberSalaryFormViewModel> model,string userId)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Total Household Income: {householdIncome}\n");
            
            sb.AppendLine($"Total Household Expences: {householdExpences}\n");
            
            foreach (var member in memberPayed)
            {
                string name = model.Where(m => m.Id == member.Key).Select(m => m.Name).First();
                
                string sanitazidName= EscapeHtml(name);

                string diff = GetDiffString(member.Key);
                
                sb.AppendLine($"-{sanitazidName} payed: {member.Value} which is {diff}.\n");
               
            }
            string summary = sb.ToString().TrimEnd();
            summary = summary.Replace("\n", "<br>");

            return summary;
        }

        private string GetDiffString(int id)
        {
            switch (memberDifferance[id])
            {
                case > 0:
                    return $"{Math.Abs(memberDifferance[id]):f2} too much";
                case < 0:
                    return $"{Math.Abs(memberDifferance[id]):f2} less";
                case  0:
                    return "exact";
               
            }
        }

        private  void GetHouseholdExpences(string userId)
        {
           householdExpences = _context.Bills.Where(b => b.UserId == userId && b.DeletedOn == null && b.IsArchived ==false).Select(b=>b.Cost).Sum();
        }
        private void GetHouseholdIncome(List<MemberSalaryFormViewModel> model)
        {
            householdIncome = model.Sum(m => m.Salary);

        }
        private void FillMembersShouldHavePayed(List<MemberSalaryFormViewModel> model)
        {
            if (householdIncome!=0)
            {
                Dictionary<int, decimal> contributionPercentage = new Dictionary<int, decimal>();
                foreach (var member in model)
                {
                    contributionPercentage.Add(member.Id, ((member.Salary / householdIncome) * 100));

                }
                foreach (var member in contributionPercentage)
                {
                    memberShouldHavePayed.Add(member.Key, (householdExpences * (member.Value / 100)));
                }
            }
            
        }
        private void FillMemberDifferance()
        {
            foreach(var member in memberPayed)
            {
                memberDifferance.Add(member.Key, (member.Value - memberShouldHavePayed[member.Key]));
            }
        }

        private void FillMembersPayed(List<MemberSalaryFormViewModel> model,string userId)
        {
            foreach(var member in model)
            {
                var payed = _context.Bills.Where(b => b.UserId == userId && b.IsArchived == false && b.DeletedOn == null && b.PayerId == member.Id).Select(b => b.Cost).Sum();
                memberPayed.Add(member.Id, payed);
            }
        }

        public async Task ArchiveBills(string userId)
        {
            var bills = await _context.Bills.Where(b => b.UserId == userId && b.DeletedOn == null && b.IsArchived == false).ToListAsync();
            foreach(var member in bills)
            {
                member.IsArchived = true;
            }
            await _context.SaveChangesAsync();
        }

        private string EscapeHtml(string html)
        {
            return html.Replace("&", "&amp;")
                       .Replace("<", "&lt;")
                       .Replace(">", "&gt;")
                       .Replace("\"", "&quot;")
                       .Replace("'", "&#39;")
                       .Replace("\u00A0", "&nbsp;")
                       .Replace("\u2264", "&le;")
                       .Replace("\u2265", "&ge;")
                       .Replace("\u2014", "&mdash;")
                       .Replace("\u2013", "&ndash;");
        }
    }
}
