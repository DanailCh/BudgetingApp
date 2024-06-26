﻿using App.Core.Enum;
using App.Core.Models.Archive.HouseholdBudget;
using App.Core.Models.BillType;
using App.Core.Models.BudgetSummary;
using App.Core.Models.HouseholdMember;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Models.Archive.MemberSalary
{
    public class AllArchivedMembersSalariesQueryModel
    {
        public int MembersSalariesPerPage { get; } = Constants.PaginationConstants.MembersSalariesPerPage;

        public string MemberId { get; init; }

        public DateTime? SalariesMonth { get; init; }

        public SalariesSorting Sorting { get; set; } = SalariesSorting.None;        

        public int CurrentPage { get; init; } = 1;

        public int ArchivedMembersSalariesCount { get; set; }

        public IEnumerable<HouseholdMemberViewModel> Members { get; set; } = null!;

        public IEnumerable<ArchiveMemberSalaryViewModel> ArchivedSalaries { get; set; } = new List<ArchiveMemberSalaryViewModel>();
    }
}
