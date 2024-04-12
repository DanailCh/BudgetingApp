// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using App.Infrastructure.Data.Models;
using HouseholdBudgetingApp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HouseholdBudgetingApp.Areas.Identity.Pages.Account.Manage
{
    public class DeletePersonalDataModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DeletePersonalDataModel> _logger;

        public DeletePersonalDataModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,ApplicationDbContext context, 
            ILogger<DeletePersonalDataModel> logger )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _context= context;
        }

        
        [BindProperty]
        public InputModel Input { get; set; }

        
        public class InputModel
        {
            
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

       
        public bool RequirePassword { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            if (User?.Identity != null && User.Identity.IsAuthenticated &&  User.IsInRole("Administrator"))
            {
                return RedirectToAction("Index", "Messages", new { area = "Admin" });
            }

            RequirePassword = await _userManager.HasPasswordAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            RequirePassword = await _userManager.HasPasswordAsync(user);
            if (RequirePassword)
            {

                if (!await _userManager.CheckPasswordAsync(user, Input.Password))
                {
                    ModelState.AddModelError(string.Empty, "Incorrect password.");
                    return Page();
                }
            }
            
            IdentityResult result = await DeleteUserAsync(user);
            var userId = await _userManager.GetUserIdAsync(user);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Unexpected error occurred deleting user.");
            }

            await _signInManager.SignOutAsync();

            _logger.LogInformation("User with ID '{UserId}' deleted themselves.", userId);

            return Redirect("~/");
        }

        private async Task<IdentityResult> DeleteUserAsync(ApplicationUser user)
        {
            var feedbackMessage = await _context.FeedbackMessages.Where(b => b.SenderId == user.Id).ToListAsync();
            foreach (var item in feedbackMessage)
            {
                item.SenderId = $"{user.UserName} - deleted user";
            }
            await _context.SaveChangesAsync();
            int billsDeleted=await _context.Bills.Where(b => b.UserId == user.Id).ExecuteDeleteAsync();
            int salariesDeleted = await _context.MemberSalaries.Where(b => b.UserId == user.Id).ExecuteDeleteAsync();
            int billTypesDeleted = await _context.BillTypes.Where(b => b.UserId == user.Id).ExecuteDeleteAsync();
            int membersDeleted = await _context.HouseholdMembers.Where(b => b.UserId == user.Id).ExecuteDeleteAsync();
            int budgetsDeleted = await _context.HouseholdBudgets.Where(b => b.UserId == user.Id).ExecuteDeleteAsync();
            int summeryDeleted = await _context.EndMonthSummaries.Where(b => b.UserId == user.Id).ExecuteDeleteAsync();
            return await _userManager.DeleteAsync(user);
        }
    }
}
