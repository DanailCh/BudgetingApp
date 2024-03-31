using App.Core.Contracts;
using App.Core.Models.Archive.Bill;
using App.Core.Models.Archive.HouseholdBudget;
using App.Core.Models.Archive.MemberSalary;
using App.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace HouseholdBudgetingApp.Controllers
{
    public class ArchiveController : BaseController
    {
        private readonly IBillService billService;
        private readonly IHouseholdService householdService;
        private readonly IBudgetSummaryService budgetSummaryService;
        private readonly IFileGeneratorService fileGeneratorService;


        public ArchiveController(
            IBillService _billService, IHouseholdService _householdService, IFileGeneratorService _fileGeneratorService, IBudgetSummaryService _budgetSummaryService)
        {
            billService = _billService;
            householdService = _householdService;
            fileGeneratorService = _fileGeneratorService;
            budgetSummaryService = _budgetSummaryService;
        }
        [HttpGet]
        public async Task<IActionResult> BillsArchive([FromQuery] AllArchivedBillsQueryModel model)
        {

            var archivedBills = await billService.AllBillsAsync(
                User.Id(),
                model.BillMonth,
                model.BillTypeId,
                model.SortingDate,
                model.SortingCost,
                model.CurrentPage,
                model.BillsPerPage
               );

            
            model.BillTypes = await billService.GetBillTypesAsync(User.Id());
            model.ArchivedBills=archivedBills.ArchivedBills;
            model.ArchivedBillsCount=archivedBills.ArchivedBillsCount;
           
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> HouseholdArchive([FromQuery] AllArchivedBudgetsQueryModel model)
        {

            var archivedBudgets = await budgetSummaryService.AllBudgetsAsync(
                User.Id(),model);

            model.ArchivedBudgets = archivedBudgets.ArchivedBudgets;
            model.ArchivedBudgetsCount = archivedBudgets.ArchivedBudgetsCount;

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> SalariesArchive([FromQuery] AllArchivedMembersSalariesQueryModel
            model)
        {

            var archivedSalaries = await householdService.AllMembersSalariesAsync(
                User.Id(),model
               );
            model.Members = await householdService.AllHouseholdMembersAsync(User.Id());
            model.ArchivedMembersSalariesCount=archivedSalaries.ArchivedMembersSalariesCount;
            model.ArchivedSalaries = archivedSalaries.ArchivedSalaries;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DownloadTextFileForBills([FromQuery] AllArchivedBillsQueryModel model)
        {
            var archivedBills = await billService.AllBillsAsync(
               User.Id(),
               model.BillMonth,
               model.BillTypeId,
               model.SortingDate,
               model.SortingCost,
               model.CurrentPage,
               model.BillsPerPage
              );
            var bills= archivedBills.ArchivedBills;
            if (!bills.Any())
            {
                return BadRequest();
            }
            string text = fileGeneratorService.GenerateFileForArchivedBills(User.Id(), bills);
            Response.Headers.Add(HeaderNames.ContentDisposition, @"attachment;filename=billsArchive.txt");
            return File(Encoding.UTF8.GetBytes(text), "text/plain");
        }
        [HttpGet]
        public async Task<IActionResult> DownloadTextFileForBudgets([FromQuery] AllArchivedBudgetsQueryModel model)
        {
            var archivedBudgets = await budgetSummaryService.AllBudgetsAsync(
                User.Id(), model);

            var budgets = archivedBudgets.ArchivedBudgets;
            if (!budgets.Any())
            {
                return BadRequest();
            }


            string text =  fileGeneratorService.GenerateFileForArchivedBudgets(User.Id(), budgets);
            Response.Headers.Add(HeaderNames.ContentDisposition, @"attachment;filename=budgetsArchive.txt");
            return File(Encoding.UTF8.GetBytes(text), "text/plain");
        }
        [HttpGet]
        public async Task<IActionResult> DownloadTextFileForSalaries([FromQuery] AllArchivedMembersSalariesQueryModel model)
        {
            var archivedSalaries = await householdService.AllMembersSalariesAsync(
                User.Id(), model
               );           
            var salaries = archivedSalaries.ArchivedSalaries;
            if (!salaries.Any())
            {
                return BadRequest();
            }


            string text = fileGeneratorService.GenerateFileForArchivedSalaries(User.Id(), salaries);
            Response.Headers.Add(HeaderNames.ContentDisposition, @"attachment;filename=salariesArchive.txt");
            return File(Encoding.UTF8.GetBytes(text), "text/plain");
        }

    }
}
