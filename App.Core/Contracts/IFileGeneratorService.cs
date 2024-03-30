using App.Core.Models.Archive.Bill;
using App.Core.Models.Archive.HouseholdBudget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Contracts
{
    public interface IFileGeneratorService
    {
        string GenerateFileForArchivedBills(string userId, IEnumerable<ArchiveBillViewModel> model);
        string GenerateFileForArchivedBudgets(string v, IEnumerable<ArchiveHouseholdBudgetViewModel> budgets);
    }
}
