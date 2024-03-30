using App.Core.Contracts;
using App.Core.Models.Bill;
using App.Core.Models.BillType;
using App.Core.Models.HouseholdMember;
using App.Infrastructure.Data.Models;
using static App.Core.Constants.MemberConstants;
using HouseholdBudgetingApp.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core.Models.Archive.MemberSalary;

namespace App.Core.Services
{
    public class HouseholdService : IHouseholdService
    {
        private readonly ApplicationDbContext _context;
        public HouseholdService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<HouseholdMemberFormViewModel>> AllHouseholdMembersAsync(string userId)
        {
            var members = await _context.HouseholdMembers.AsNoTracking().Where(m => m.UserId == userId && m.DeletedOn==null).Select(m => new HouseholdMemberFormViewModel
            {
                Id = m.Id,
                Name = m.Name,
               
            }).ToListAsync();
            return members;
        }

        public Task<ArchiveMemberSalaryQueryModel> AllMembersSalariesAsync(string userId, AllArchivedMembersSalariesQueryModel model)
        {
            throw new NotImplementedException();
        }

        public async Task CreateHouseholdMemberAsync(HouseholdMemberFormViewModel model, string userId)
        {
            var member = new HouseholdMember
            {
                
                Name=model.Name,
                UserId = userId,
                
            };
            await _context.HouseholdMembers.AddAsync(member);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteHouseholdMemberByIdAsync(int id)
        {
            
            var member = await _context.HouseholdMembers.FindAsync(id);
            if (member != null)
            {
                member.DeletedOn= DateTime.Now;
                await _context.SaveChangesAsync();
            }

            
        }

        

        public async Task<HouseholdMemberFormViewModel> FindHouseholdMemberByIdAsync(int id)
        {
           HouseholdMemberFormViewModel foundMember;
            var member = await _context.HouseholdMembers.FindAsync(id);
            if (member == null)
            {
                return null;
            }

            return foundMember = new HouseholdMemberFormViewModel()
            {
               Name = member.Name,
            };
        }

        public async Task<bool> MemberBelongsToUserAsync(int id, string userId)
        {
            return await _context.HouseholdMembers.AnyAsync(hm=>hm.UserId == userId&& hm.Id==id);
        }

        public async Task<bool> MemberExistsAsync(int id)
        {
            return await _context.HouseholdMembers.AnyAsync(hm=>hm.Id == id);
        }

        public async Task<bool> MinimumMembersAsync(string userId)
        {
            return (await _context.HouseholdMembers.Where(hm => hm.UserId == userId && hm.DeletedOn == null).CountAsync()) < MinimumMembers;
        }

        public async Task<bool> OverMembersLimitAsync(string userId)
        {
            return (await _context.HouseholdMembers.Where(hm => hm.UserId == userId && hm.DeletedOn == null).CountAsync()) >= MaximumMembers; 
            
        }
    }
}
