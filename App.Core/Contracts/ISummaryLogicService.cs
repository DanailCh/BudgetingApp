using App.Core.Models.BudgetSummary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Contracts
{
    public interface ISummaryLogicService
    {
         string GetSummary(List<MemberSalaryFormModel> model, string userId);
        Task ArchiveBills(string userId);
    }
}
