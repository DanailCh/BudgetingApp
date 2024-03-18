using App.Core.Contracts;
using App.Core.Models.Bill;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HouseholdBudgetingApp.Controllers
{
    public class BillController:BaseController
    {
        private readonly IBillService billService;

        

        public BillController(
            IBillService _billService)
        {
            billService = _billService;
            
        }

        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = User.Id;
            var model = await billService.AllBillsAsync(userId.ToString());

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var userId = User.Id;
            var model = new BillFormModel()
            {
                BillTypes = await billService.GetBillTypesAsync(userId.ToString())
            };
            return View(model);
        }
    }
}
