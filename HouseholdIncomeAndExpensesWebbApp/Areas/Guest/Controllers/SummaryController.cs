using App.Core.Contracts;
using App.Core.Models.BudgetSummary;
using App.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static App.Core.Constants.TempDataMessagesConstants;
using static App.Core.Constants.TempDataErrorMessagesConstants;

namespace HouseholdBudgetingApp.Areas.Guest.Controllers
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

            if (await householdService.UnderMinimumMembersAsync(User.Id()))
            {
                TempData["ErrorMessage"] = NotEnoughMembersMessage;
                return RedirectToAction(nameof(Index),"Household");
            }
            if (await budgetSummaryService.HasBillsAsync(User.Id())==false)
            {
                TempData["ErrorMessage"] = NoBillsMessage;
                return RedirectToAction(nameof(Index));
            }
            if (await budgetSummaryService.NotAllBillsPayedAsync(User.Id()))
            {
                TempData["ErrorMessage"] = BillsNotPayedMessage;
                return RedirectToAction(nameof(Index), "Bill");
            }
            var model = await budgetSummaryService.GetMemberSalaryFormModelsAsync(User.Id());


            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] List<MemberSalaryFormViewModel> model)
        {
            
            if (budgetSummaryService.HouseholdIncomeIsZero(model))
            {
                TempData["ErrorMessage"] = IncomeCantBeZeroMessage;
                return View(model);
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            await budgetSummaryService.CreateSummary(model, User.Id());
            TempData["Message"] = SummaryCreatedMessage;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Resolve(int id)
        {
            if (!(await budgetSummaryService.AllSummariesAsync(User.Id())).Any(b => b.Id == id))
            {
                return NotFound();
            }            

            await budgetSummaryService.ResolveSummary(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
