using App.Core.Contracts;
using App.Core.Models.BudgetSummary;
using App.Core.Services;
using App.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HouseholdBudgetingApp.Controllers
{
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
            if (await budgetSummaryService.NotAllBillsPayedAsync(User.Id()))
            {
                TempData["Message"] = "All Bills must be payed in order to Summarize month!";
                return RedirectToAction(nameof(Index), nameof(Bill));
            }
            var model = await budgetSummaryService.GetMemberSalaryFormModelsAsync(User.Id());
           
            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm]List<MemberSalaryFormModel> model)
        {
            foreach(var member in model)
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
    }
}
