using App.Core.Contracts;
using App.Core.Models.Bill;
using App.Infrastructure.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Security.Claims;

namespace HouseholdBudgetingApp.Controllers
{
    public class BillController:BaseController
    {
        private readonly IBillService billService;
        private readonly IHouseholdService householdService;



        public BillController(
            IBillService _billService,IHouseholdService _householdService)
        {
            billService = _billService;
            householdService = _householdService;
            
        }

        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = User.Id();
            var model = await billService.AllBillsAsync(userId);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var userId = User.Id();
            var model = new BillFormModel()
            {
                BillTypes = await billService.GetBillTypesAsync(userId),
                Payers=await householdService.GetHouseholdMembersAsync(userId)
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult>Add(BillFormModel model)
        {
            var userId=User.Id();
            if (!(await billService.GetBillTypesAsync(userId)).Any(b => b.Id == model.BillTypeId))
            {
                ModelState.AddModelError(nameof(model.BillTypeId), "Type does not exist.");
            }

            DateTime date = DateTime.Now;
            

            if (!DateTime.TryParseExact(
                model.Date,"yyyy-MM-dd H:mm",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out date))
            {
                ModelState
                    .AddModelError(nameof(model.Date), $"Invalid date! Format must be: yyyy-MM-dd H:mm");
            }

            if (!ModelState.IsValid)
            {
                model.BillTypes = await billService.GetBillTypesAsync(userId);
                model.Payers = await householdService.GetHouseholdMembersAsync(userId);

                return View(model);
            }

            await billService.CreateBillAsync(model, userId);
            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = User.Id();
            var model=await billService.FindBillByIdAsync(id);
            model.BillTypes = await billService.GetBillTypesAsync(userId);
            model.Payers = await householdService.GetHouseholdMembersAsync(userId);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(BillFormModel model,int id)
        {
            var userId = User.Id();
            if (!(await billService.GetBillTypesAsync(userId)).Any(b => b.Id == model.BillTypeId))
            {
                ModelState.AddModelError(nameof(model.BillTypeId), "Type does not exist.");
            }

            DateTime date = DateTime.Now;


            if (!DateTime.TryParseExact(
                model.Date, "yyyy-MM-dd H:mm",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out date))
            {
                ModelState
                    .AddModelError(nameof(model.Date), $"Invalid date! Format must be: yyyy-MM-dd H:mm");
            }

            if (!ModelState.IsValid)
            {
                model.BillTypes = await billService.GetBillTypesAsync(userId);
                model.Payers = await householdService.GetHouseholdMembersAsync(userId);

                return View(model);
            }

            await billService.EditBillByIdAsync(model, id);
            return RedirectToAction("Index");
        }
    }
}
