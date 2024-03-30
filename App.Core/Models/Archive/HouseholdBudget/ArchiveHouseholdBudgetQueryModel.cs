using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Models.Archive.HouseholdBudget
{
    public class ArchiveHouseholdBudgetQueryModel
    {
        public int ArchivedBudgetsCount { get; set; }

        public IEnumerable<ArchiveHouseholdBudgetViewModel> ArchivedBudgets { get; set; } = new List<ArchiveHouseholdBudgetViewModel>();
    }
}
