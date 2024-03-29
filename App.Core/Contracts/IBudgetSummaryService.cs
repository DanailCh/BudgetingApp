using App.Core.Models.BudgetSummary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Contracts
{
    public interface IBudgetSummaryService
    {
        Task<List<MemberSalaryFormModel>> GetMemberSalaryFormModelsAsync(string userId);
        Task<IEnumerable<SummaryViewModel>>AllSummariesAsync(string userId);
        Task<bool>NotAllBillsPayedAsync(string userId);
        Task CreateSummary(List<MemberSalaryFormModel> models,string userId);
        Task ResolveSummary(int id);

    }
}
