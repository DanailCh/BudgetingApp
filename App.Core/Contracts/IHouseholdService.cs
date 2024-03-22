using App.Core.Models.Bill;
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
        Task<IEnumerable<HouseholdMemberViewModel>> AllHouseholdMembersAsync(string userId);

        Task CreateHouseholdMemberAsync(HouseholdMemberViewModel model, string userId);
        Task<IEnumerable<HouseholdMemberViewModel>> GetHouseholdMembersAsync(string userId);

        Task EditHouseholdMemberByIdAsync(HouseholdMemberViewModel model, int id);
        Task DeleteHouseholdMemberByIdAsync(int id);
    }
}
