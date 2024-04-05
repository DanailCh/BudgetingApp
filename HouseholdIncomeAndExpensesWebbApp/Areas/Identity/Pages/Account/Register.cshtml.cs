// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using App.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace HouseholdBudgetingApp.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;


        public RegisterModel(
            UserManager<ApplicationUser> userManager,            
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger, RoleManager<IdentityRole> roleManager, IUserStore<ApplicationUser> userStore)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _roleManager = roleManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
        }


        [BindProperty]
        public InputModel Input { get; set; }
       
        public string ReturnUrl { get; set; }        
        
        public class InputModel
        {
            
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(15, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
            [Display(Name = "UserName")]
            public string UserName { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }


        public async Task<IActionResult> OnGetAsync(string returnUrl = null)
        {
            if (User?.Identity != null && User.Identity.IsAuthenticated&& (User.IsInRole("Guest")||User.IsInRole("Administrator")))
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ReturnUrl = returnUrl; 
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
           
            if (ModelState.IsValid)
            {
                var user = CreateUser();
                if (User.IsMasterAdmin())
                {
                    user.PasswordSetupRequired = true;
                }
                await _userStore.SetUserNameAsync(user, Input.UserName, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    IdentityRole defaultRole;
                    if (User.IsMasterAdmin())
                    {
                        _logger.LogInformation("Admin User created.");
                        defaultRole = _roleManager.FindByNameAsync("Administrator").Result;
                    }
                    else
                    {
                        _logger.LogInformation("User created a new account with password.");
                        defaultRole = _roleManager.FindByNameAsync("Guest").Result;
                    }
                   
                    if(defaultRole != null)
                    {
                        IdentityResult roleResult = await _userManager.AddToRoleAsync(user, defaultRole.Name);
                    }

                    if (User.IsMasterAdmin())
                    {
                        return RedirectToAction("Index", "Admin", new { area = "Admin" });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                   
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            
            return Page();
        }

        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                    $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }
        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)_userStore;
        }
    }
}
