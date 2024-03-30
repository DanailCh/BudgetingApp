using App.Core.Contracts;
using App.Core.Models.Archive.Bill;
using App.Core.Models.Archive.HouseholdBudget;
using App.Infrastructure.Data.Models;
using HouseholdBudgetingApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Services
{
    public class FileGeneratorService : IFileGeneratorService
    {
        private readonly ApplicationDbContext _context;

        public FileGeneratorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public string GenerateFileForArchivedBills(string userId, IEnumerable<ArchiveBillViewModel> model)
        {
            int maxBillTypeWidth = model.Max(b => b.BillTypeName.Length);
            int maxDateWidth = model.Max(b => b.Date.ToString("MMMM yyyy").Length);
            int maxCostWidth = model.Max(b => b.Cost.ToString().Length);

            
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"| {"Bill Type".PadRight(maxBillTypeWidth)} | {"Month".PadRight(maxDateWidth)} | {"Cost".PadRight(maxCostWidth)} |");
            sb.AppendLine($"| {new string('_', maxBillTypeWidth)} | {new string('_', maxDateWidth)} | {new string('_', maxCostWidth)} |");

            foreach (var item in model)
            {
                string monthYear = item.Date.ToString("MMMM yyyy");
                sb.AppendLine($"| {item.BillTypeName.PadRight(maxBillTypeWidth)} | {monthYear.PadRight(maxDateWidth)} | {item.Cost.ToString().PadRight(maxCostWidth)} |");
                sb.AppendLine($"| {new string('-', maxBillTypeWidth)} | {new string('-', maxDateWidth)} | {new string('-', maxCostWidth)} |");
            }
            return sb.ToString();
        }

        public string GenerateFileForArchivedBudgets(string v, IEnumerable<ArchiveHouseholdBudgetViewModel> model)
        {
            int maxDateWidth = model.Max(b => b.Date.ToString("MMMM yyyy").Length);
            int maxIncomeWidth = model.Max(b => b.Income.ToString().Length);
            int maxExpencesWidth = model.Max(b => b.Expences.ToString().Length);


            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"| {"Month".PadRight(maxDateWidth)} | {"Income".PadRight(maxIncomeWidth)} | {"Expences".PadRight(maxExpencesWidth)} |");
            sb.AppendLine($"| {new string('_', maxDateWidth)} | {new string('_', maxIncomeWidth)} | {new string('_', maxExpencesWidth)} |");

            foreach (var item in model)
            {
                string monthYear = item.Date.ToString("MMMM yyyy");
                sb.AppendLine($"| {monthYear.PadRight(maxDateWidth)} | {item.Income.ToString().PadRight(maxIncomeWidth)} | {item.Expences.ToString().PadRight(maxExpencesWidth)} |");
                sb.AppendLine($"| {new string('-', maxDateWidth)} | {new string('-', maxIncomeWidth)} | {new string('-', maxExpencesWidth)} |");
            }
            return sb.ToString();
        }
    }
}
