using App.Core.Contracts;
using App.Core.Models.Bill;
using App.Core.Models.BillType;
using App.Core.Models.HouseholdMember;
using App.Infrastructure.Data.Models;
using HouseholdBudgetingApp.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Services
{
    public class HouseholdService : IHouseholdService
    {
        private readonly ApplicationDbContext _context;
        public HouseholdService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<HouseholdMemberViewModel>> AllHouseholdMembersAsync(string userId)
        {
            var members = await _context.HouseholdMembers.AsNoTracking().Where(m => m.UserId == userId).Select(m => new HouseholdMemberViewModel
            {
                Id = m.Id,
                Name = m.Name,
               
            }).ToListAsync();
            return members;
        }

        public async Task CreateHouseholdMemberAsync(HouseholdMemberViewModel model, string userId)
        {
            var member = new HouseholdMember
            {
                Id = model.Id,
                Name=model.Name,
                UserId = userId,
                
            };
            await _context.HouseholdMembers.AddAsync(member);
            await _context.SaveChangesAsync();
        }

        public Task DeleteHouseholdMemberByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task EditHouseholdMemberByIdAsync(HouseholdMemberViewModel model, int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<HouseholdMemberViewModel>> GetHouseholdMembersAsync(string userId)
        {
            var types = await _context
                .HouseholdMembers.AsNoTracking()
                .Where(b => b.UserId == userId)
                .Select(t => new HouseholdMemberViewModel()
                {
                    Id = t.Id,
                    Name = t.Name
                }).ToListAsync();
            return types;

        }
    }
}
