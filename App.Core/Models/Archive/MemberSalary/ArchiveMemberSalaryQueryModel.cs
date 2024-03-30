using App.Core.Models.Archive.HouseholdBudget;
using App.Core.Models.BudgetSummary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Models.Archive.MemberSalary
{
    public class ArchiveMemberSalaryQueryModel
    {
        public int ArchivedMembersSalariesCount { get; set; }

        public IEnumerable<MemberSalaryFormViewModel> ArchivedBudgets { get; set; } = new List<MemberSalaryFormViewModel>();
    }
}
