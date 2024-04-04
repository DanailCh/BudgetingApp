using App.Core.Contracts;
using App.Core.Models.Bill;
using App.Infrastructure.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Security.Claims;

namespace HouseholdBudgetingApp.Areas.Guest.Controllers
{
    [Area("Guest")]
    [Authorize(Roles = "Guest")]
    public class BillController : BaseController
    {
        private readonly IBillService billService;
        private readonly IHouseholdService householdService;



        public BillController(
            IBillService _billService, IHouseholdService _householdService)
        {
            billService = _billService;
            householdService = _householdService;

        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var model = await billService.AllCurentMonthBillsAsync(User.Id(), await billService.GetDateAsync(User.Id()));
            foreach (var item in model) { item.Payers = await householdService.AllHouseholdMembersAsync(User.Id()); }
            ViewBag.Date = await billService.GetFormatedDateAsync(User.Id());
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {

            ViewBag.Date = await billService.GetFormatedDateAsync(User.Id());
            var model = new BillFormModel()
            {
                BillTypes = await billService.GetBillTypesAsync(User.Id()),
                Payers = await householdService.AllHouseholdMembersAsync(User.Id()),
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(BillFormModel model)
        {

            if (!(await billService.GetBillTypesAsync(User.Id())).Any(b => b.Id == model.BillTypeId))
            {
                ModelState.AddModelError(nameof(model.BillTypeId), "Bill Type does not exist.");
            }
            
            if (model.PayerId!=null&&(await householdService.AllHouseholdMembersAsync(User.Id())).Any(m=>m.Id==model.PayerId)==false)
            {
               ModelState.AddModelError(nameof(model.PayerId), "Member does not exist.");
            }

            if (!ModelState.IsValid)
            {
                model.BillTypes = await billService.GetBillTypesAsync(User.Id());
                model.Payers = await householdService.AllHouseholdMembersAsync(User.Id());

                return View(model);
            }

            await billService.CreateBillAsync(model, User.Id());
            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            if (await billService.BillExistsAsync(id) == false)
            {
                return NotFound();
            }
            if (await billService.BillIsPayedAsync(id))
            {
                TempData["ErrorMessage"] = "Cannot edit details of a bill that has already been paid.";
                return StatusCode(StatusCodes.Status409Conflict);
            }

            if (await billService.BillBelongsToUserAsync(id, User.Id()) == false)
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }

            var model = await billService.FindBillByIdAsync(id);
            model.BillTypes = await billService.GetBillTypesAsync(User.Id());
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(BillFormModel model, int id)
        {
            if (await billService.BillExistsAsync(id) == false)
            {
                return NotFound();
            }
            if (await billService.BillIsPayedAsync(id))
            {
                TempData["ErrorMessage"] = "Cannot edit details of a bill that has already been paid.";
                return StatusCode(StatusCodes.Status409Conflict);
            }

            if (await billService.BillBelongsToUserAsync(id, User.Id()) == false)
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }
            if (!(await billService.GetBillTypesAsync(User.Id())).Any(b => b.Id == model.BillTypeId))
            {
                ModelState.AddModelError(nameof(model.BillTypeId), "Bill Type does not exist.");
            }


            if (!ModelState.IsValid)
            {
                model.BillTypes = await billService.GetBillTypesAsync(User.Id());

                return View(model);
            }

            await billService.EditBillByIdAsync(model, id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Pay(BillViewModel model, int id)
        {
            if (await billService.BillExistsAsync(id) == false)
            {
                return NotFound();
            }
            if (await billService.BillIsPayedAsync(id))
            {
                return StatusCode(StatusCodes.Status409Conflict);
            }
            if (await billService.BillBelongsToUserAsync(id, User.Id()) == false)
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }


            if (!(await householdService.AllHouseholdMembersAsync(User.Id())).Any(m => m.Id == model.PayerId))
            {
                ModelState.AddModelError(nameof(model.PayerId), "Household Member does not exist.");
            }
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }



            await billService.PayBillAsync(model, id);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (await billService.BillExistsAsync(id) == false)
            {
                return NotFound();
            }

            if (await billService.BillBelongsToUserAsync(id, User.Id()) == false)
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }
            await billService.DeleteBillByIdAsync(id);

            return RedirectToAction("Index");
        }
    }
}
