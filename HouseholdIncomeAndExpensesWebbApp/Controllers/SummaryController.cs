using App.Core.Contracts;
using App.Core.Models.BudgetSummary;
using App.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HouseholdBudgetingApp.Controllers
{
    public class SummaryController : BaseController
    {
        private readonly IBudgetSummaryService budgetSummaryService;
        public SummaryController(IBudgetSummaryService budgetSummaryService)
        {
            this.budgetSummaryService = budgetSummaryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = User.Id();
            var model = await budgetSummaryService.GetAllEndMontSummariesAsync(userId);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var userId = User.Id();
            var model = await budgetSummaryService.AllHouseholdMembersAsync(userId);
            DateTime date = DateTime.Now;
            DateTime monthYearOnly = new DateTime(date.Year, date.Month, 1);

            string formattedDate = monthYearOnly.ToString("MMMM yyyy");
            ViewBag.Date = formattedDate;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(IEnumerable<SummaryFormModel> model)
        {
            var userId = User.Id();
            if (!ModelState.IsValid)
            {               
                return View(model);
            }
            await budgetSummaryService.CreateSummary(model, userId);
            return RedirectToAction("Index");
        }
    }
}
