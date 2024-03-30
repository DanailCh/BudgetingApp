using App.Core.Contracts;
using App.Core.Models.Bill;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HouseholdBudgetingApp.Controllers
{
    public class ArchiveController : BaseController
    {
        private readonly IBillService billService;
        private readonly IHouseholdService householdService;



        public ArchiveController(
            IBillService _billService, IHouseholdService _householdService)
        {
            billService = _billService;
            householdService = _householdService;

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
    }
}
