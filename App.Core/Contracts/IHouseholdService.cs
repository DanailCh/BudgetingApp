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
        Task<IEnumerable<HouseholdMemberFormViewModel>> AllHouseholdMembersAsync(string userId);

        Task CreateHouseholdMemberAsync(HouseholdMemberFormViewModel model, string userId);
        Task<IEnumerable<HouseholdMemberFormViewModel>> GetHouseholdMembersAsync(string userId);

        Task EditHouseholdMemberByIdAsync(HouseholdMemberFormViewModel model, int id);
        Task<HouseholdMemberFormViewModel> FindHouseholdMemberByIdAsync(int id);
        Task DeleteHouseholdMemberByIdAsync(int id);
    }
}
