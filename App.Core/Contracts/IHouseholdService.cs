using App.Core.Enum;
using App.Core.Models.Archive.Bill;
using App.Core.Models.Archive.HouseholdBudget;
using App.Core.Models.Archive.MemberSalary;
using App.Core.Models.BillType;
using App.Core.Models.HouseholdMember;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Contracts
{
    public interface IHouseholdService
    {
        Task<ArchiveMemberSalaryQueryModel> AllMembersSalariesAsync(string userId,AllArchivedMembersSalariesQueryModel model);
        Task<IEnumerable<HouseholdMemberFormViewModel>> AllHouseholdMembersAsync(string userId);
       
        Task CreateHouseholdMemberAsync(HouseholdMemberFormViewModel model, string userId);
        
        Task<bool> OverMembersLimitAsync(string userId);
        Task<bool> MinimumMembersAsync(string userId);

        Task<bool> MemberExistsAsync(int id);
        Task<bool> MemberBelongsToUserAsync(int id,string userId);        
        Task DeleteHouseholdMemberByIdAsync(int id);
    }
}
