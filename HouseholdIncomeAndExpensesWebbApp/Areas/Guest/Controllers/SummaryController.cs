using App.Core.Contracts;
using App.Core.Models.BudgetSummary;
using App.Core.Services;
using App.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;
using System.Security.Claims;

namespace HouseholdBudgetingApp.Areas.Guest.Controllers
{
    [Area("Guest")]
    public class SummaryController : BaseController
    {
        private readonly IBudgetSummaryService budgetSummaryService;
        private readonly IHouseholdService householdService;
        private readonly IBillService billService;
        public SummaryController(IBudgetSummaryService budgetSummaryService, IHouseholdService householdService, IBillService billService)
        {
            this.budgetSummaryService = budgetSummaryService;
            this.householdService = householdService;
            this.billService = billService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var model = await budgetSummaryService.AllSummariesAsync(User.Id());
            ViewBag.Date = await billService.GetFormatedDateAsync(User.Id());
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {

            if (await householdService.MinimumMembersAsync(User.Id()))
            {
                TempData["Message"] = "Not enough members";
                return RedirectToAction(nameof(Index),"Household");
            }
            if (await budgetSummaryService.HasBillsAsync(User.Id())==false)
            {
                TempData["Message"] = "No Bills to Summarize";
                return RedirectToAction(nameof(Index));
            }
            if (await budgetSummaryService.NotAllBillsPayedAsync(User.Id()))
            {
                TempData["Message"] = "All Bills must be payed in order to Summarize month!";
                return RedirectToAction(nameof(Index), nameof(Bill));
            }
            var model = await budgetSummaryService.GetMemberSalaryFormModelsAsync(User.Id());


            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] List<MemberSalaryFormViewModel> model)
        {
            foreach (var member in model)
            {
                if (await householdService.MemberExistsAsync(member.Id) == false)
                {
                    return BadRequest();
                }

                if (await householdService.MemberBelongsToUserAsync(member.Id, User.Id()) == false)
                {
                    return Unauthorized();
                }
            }


            if (!ModelState.IsValid)
            {
                return View(model);
            }
            await budgetSummaryService.CreateSummary(model, User.Id());
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Resolve(int id)
        {
            if (await budgetSummaryService.SummaryExistsAsync(id) == false)
            {
                return BadRequest();
            }

            if (await budgetSummaryService.SummaryBelongsToUserAsync(id, User.Id()) == false)
            {
                return Unauthorized();
            }

            await budgetSummaryService.ResolveSummary(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
