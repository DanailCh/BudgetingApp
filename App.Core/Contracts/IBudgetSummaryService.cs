using App.Core.Enum;
using App.Core.Models.Archive.Bill;
using App.Core.Models.Archive.HouseholdBudget;
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
        Task<ArchiveHouseholdBudgetQueryModel> AllBudgetsAsync(string userId,AllArchivedBudgetsQueryModel model);
        Task<List<MemberSalaryFormViewModel>> GetMemberSalaryFormModelsAsync(string userId);
        Task<IEnumerable<SummaryViewModel>>AllSummariesAsync(string userId);
        Task<bool>NotAllBillsPayedAsync(string userId);
        Task<bool> HasBillsAsync(string userId);
        Task CreateSummary(List<MemberSalaryFormViewModel> models,string userId);
        Task ResolveSummary(int id);
        Task<bool> SummaryExistsAsync(int id);
        Task<bool> SummaryBelongsToUserAsync(int id,string userId);

    }
}
