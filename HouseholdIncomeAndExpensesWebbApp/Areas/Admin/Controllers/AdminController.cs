using App.Core.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace HouseholdBudgetingApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "MasterAdmin")]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model=await _adminService.AllAdminsAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            if (await _adminService.AdminExistsAsync(id)==false)
            {
                return NotFound();
            }
            await _adminService.DeleteAdminAsync(id);
            return RedirectToAction("Index");
        }
    }
}
