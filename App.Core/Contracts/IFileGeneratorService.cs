using App.Core.Models.Archive.Bill;
using App.Core.Models.Archive.HouseholdBudget;
using App.Core.Models.Archive.MemberSalary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Contracts
{
    public interface IFileGeneratorService
    {
        string GenerateFileForArchivedBills(IEnumerable<ArchiveBillViewModel> model);
        string GenerateFileForArchivedBudgets( IEnumerable<ArchiveHouseholdBudgetViewModel> budgets);
        string GenerateFileForArchivedSalaries( IEnumerable<ArchiveMemberSalaryViewModel> salaries);
    }
}
