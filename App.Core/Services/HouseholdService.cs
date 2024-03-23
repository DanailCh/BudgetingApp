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
        public async Task<IEnumerable<HouseholdMemberFormViewModel>> AllHouseholdMembersAsync(string userId)
        {
            var members = await _context.HouseholdMembers.AsNoTracking().Where(m => m.UserId == userId && m.DeletedOn==null).Select(m => new HouseholdMemberFormViewModel
            {
                Id = m.Id,
                Name = m.Name,
               
            }).ToListAsync();
            return members;
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

        public async Task EditHouseholdMemberByIdAsync(HouseholdMemberFormViewModel model, int id)
        {
            var member = await _context.HouseholdMembers.FindAsync(id);
            if (member != null)
            {
                member.Name = model.Name;
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

        public async Task<IEnumerable<HouseholdMemberFormViewModel>> GetHouseholdMembersAsync(string userId)
        {
            var members = await _context
                .HouseholdMembers.AsNoTracking()
                .Where(m => m.UserId == userId&&m.DeletedOn==null)
                .Select(m => new HouseholdMemberFormViewModel()
                {
                    Id = m.Id,
                    Name = m.Name
                }).ToListAsync();
            return members;

        }
    }
}
