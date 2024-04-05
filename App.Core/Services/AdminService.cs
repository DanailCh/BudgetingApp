using App.Core.Contracts;
using App.Core.Models.Admin;
using App.Infrastructure.Data.Models;
using HouseholdBudgetingApp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Services
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<ApplicationUser> _userManager;       
        private readonly ApplicationDbContext _context;
        public AdminService(ApplicationDbContext context,UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
           
        }

        public async Task<bool> AdminExistsAsync(string adminId)
        {
            return await _context.Users.AnyAsync(u=>u.Id == adminId);
        }

        public async Task<IEnumerable<AdminViewModel>> AllAdminsAsync()
        {
            var allAdmins = await _context.Users
             .Join(_context.UserRoles,
                   u => u.Id,
                   ur => ur.UserId,
                   (u, ur) => new { User = u, RoleId = ur.RoleId })
             .Join(_context.Roles,
                   ur => ur.RoleId,
                   r => r.Id,
                   (ur, r) => new { User = ur.User, RoleName = r.Name })
             .Where(ur => ur.RoleName == "Administrator")
             .Select(ur => ur.User).Select(user => new AdminViewModel()
             {
                 Id = user.Id,
                 UserName = user.UserName,
                 Email = user.Email,
             }).ToListAsync();
            return allAdmins;
        }

        public async Task DeleteAdminAsync(string adminId)
        {
            var adminToDelete=await _context.Users.FirstOrDefaultAsync(u=>u.Id==adminId);
            if (adminToDelete!=null)
            {
                await _userManager.DeleteAsync(adminToDelete);
            }
        }
    }
}
