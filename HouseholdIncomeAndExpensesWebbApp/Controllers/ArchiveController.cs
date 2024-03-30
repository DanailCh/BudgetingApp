using App.Core.Contracts;
using App.Core.Models.Bill;
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
        private readonly IFileGeneratorService fileGeneratorService;


        public ArchiveController(
            IBillService _billService, IHouseholdService _householdService, IFileGeneratorService _fileGeneratorService)
        {
            billService = _billService;
            householdService = _householdService;
            fileGeneratorService = _fileGeneratorService;
        }
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] AllArchivedBillsQueryModel model)
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
        public async Task<IActionResult> DownloadTextFile([FromQuery] AllArchivedBillsQueryModel model)
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

            string text = await fileGeneratorService.GenerateFileForArchivedBills(User.Id(), bills);
            Response.Headers.Add(HeaderNames.ContentDisposition, @"attachment;filename=file.txt");
            return File(Encoding.UTF8.GetBytes(text), "text/plain");
        }

    }
}
