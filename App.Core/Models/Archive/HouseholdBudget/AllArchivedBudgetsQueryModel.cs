using App.Core.Enum;
using App.Core.Models.Archive.Bill;
using App.Core.Models.BillType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Models.Archive.HouseholdBudget
{
    public class AllArchivedBudgetsQueryModel
    {
        public int BudgetsPerPage { get; } = Constants.ArchiveConstants.BudgetsPerPage;

        public DateTime? BudgetMonth { get; init; }

        public BudgetSorting Sorting { get; set; } = BudgetSorting.None;        

        public int CurrentPage { get; init; } = 1;

        public int ArchivedBudgetsCount { get; set; }        

        public IEnumerable<ArchiveHouseholdBudgetViewModel> ArchivedBudgets { get; set; } = new List<ArchiveHouseholdBudgetViewModel>();
    }
}
