﻿using App.Core.Models.BudgetSummary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Contracts
{
    public interface ISummaryLogicService
    {
         Task<string> GetSummaryAsync(List<MemberSalaryFormViewModel> model, string userId,DateTime date);
        Task ArchiveBillsAsync(string userId);
    }
}
