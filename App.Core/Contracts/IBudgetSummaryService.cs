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
        Task<IEnumerable<SummaryFormModel>> AllHouseholdMembersAsync(string userId);
        Task<IEnumerable<SummaryViewModel>>GetAllEndMontSummariesAsync(string userId);
        Task CreateSummary(IEnumerable<SummaryFormModel> summaryFormModels,string userId);
        Task ResolveSummarie(int id);

    }
}
