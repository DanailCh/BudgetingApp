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
using App.Core.Enum;
using App.Core.Models.Archive.HouseholdBudget;
using App.Core.Models.BudgetSummary;

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
            var members = await _context.HouseholdMembers.AsNoTracking().Where(m => m.UserId == userId && m.DeletedOn==null).Select(m => new HouseholdMemberViewModel
            {
                Id = m.Id,
                Name = m.Name,
               
            }).ToListAsync();
            return members;
        }        


        public async Task<ArchiveMemberSalaryQueryModel> AllMembersSalariesAsync(string userId, AllArchivedMembersSalariesQueryModel model)
        {
            var allSalaries = _context.MemberSalaries.AsNoTracking().Where(b => b.UserId == userId);

            if (model.SalariesMonth != null)
            {
                allSalaries = allSalaries
                    .Where(b => b.Date == model.SalariesMonth);

            }
            if (model.MemberId != 0)
            {
                allSalaries = allSalaries
                    .Where(b => b.UserId == userId  && b.HouseholdMemberId == model.MemberId);

            }
            if (model.MemberId != 0 && model.SalariesMonth != null)
            {
                model.Sorting = SalariesSorting.None;
            }


            switch (model.Sorting)
            {
                case SalariesSorting.HighestFirst:
                    allSalaries = allSalaries
                              .OrderByDescending(b => b.Salary);
                    break;
                case SalariesSorting.LowestFirst:
                    allSalaries = allSalaries
                              .OrderBy(b => b.Salary);
                    break;
                case SalariesSorting.None:
                    allSalaries = allSalaries
                              .OrderByDescending(b => b.Id);
                    break;

            }
            var salariesToShow = await allSalaries
               .Skip((model.CurrentPage - 1) * model.MembersSalariesPerPage)
               .Take(model.MembersSalariesPerPage)
               .Select(b => new ArchiveMemberSalaryViewModel()
               {
                   Id = b.Id,
                   Salary = b.Salary,
                   Name=b.HouseholdMember.Name,
                   Date = b.Date,
               })
               .ToListAsync();
            var salariesToDownload= await allSalaries               
               .Select(b => new ArchiveMemberSalaryViewModel()
               {
                   Id = b.Id,
                   Salary = b.Salary,
                   Name = b.HouseholdMember.Name,
                   Date = b.Date,
               })
               .ToListAsync();

            int totalArchivedSalaries = await allSalaries.CountAsync();

            return new ArchiveMemberSalaryQueryModel()
            {
                ArchivedSalariesToDownload=salariesToDownload,
                ArchivedSalaries = salariesToShow,
                ArchivedMembersSalariesCount = totalArchivedSalaries,
            };
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
