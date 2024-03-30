using App.Core.Contracts;
using App.Core.Models.Bill;
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

        public async Task<string> GenerateFileForArchivedBills(string userId, IEnumerable<ArchiveBillViewModel> model)
        {
            int maxBillTypeWidth = model.Max(b => b.BillTypeName.Length);
            int maxDateWidth = model.Max(b => b.Date.ToString("MMMM yyyy").Length);
            int maxCostWidth = model.Max(b => b.Cost.ToString().Length);

            
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"| {"Bill Type".PadRight(maxBillTypeWidth)} | {"Month".PadRight(maxDateWidth)} | {"Cost".PadRight(maxCostWidth)} |");
            sb.AppendLine($"| {new string('-', maxBillTypeWidth)} | {new string('-', maxDateWidth)} | {new string('-', maxCostWidth)} |");

            foreach (var item in model)
            {
                string monthYear = item.Date.ToString("MMMM yyyy");
                sb.AppendLine($"| {item.BillTypeName.PadRight(maxBillTypeWidth)} | {monthYear.PadRight(maxDateWidth)} | {item.Cost.ToString().PadRight(maxCostWidth)} |");
                sb.AppendLine($"| {new string('-', maxBillTypeWidth)} | {new string('-', maxDateWidth)} | {new string('-', maxCostWidth)} |");
            }
            return sb.ToString();
        }
    }
}
