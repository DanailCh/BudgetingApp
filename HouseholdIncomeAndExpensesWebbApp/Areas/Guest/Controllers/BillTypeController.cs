using App.Core.Contracts;
using App.Core.Models.Bill;
using App.Core.Models.BillType;
using App.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Security.Claims;

namespace HouseholdBudgetingApp.Areas.Guest.Controllers
{
    
    public class BillTypeController : BaseController
    {
        private readonly IBillTypeService billTypeService;

        public BillTypeController(
            IBillTypeService _billTypeService)
        {
            billTypeService = _billTypeService;

        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = new BillTypeFormViewModel();
            model.CustomBillTypes = await billTypeService.AllCustomBillTypesAsync(User.Id());
            return View(model);
        }        

        [HttpPost]
        public async Task<IActionResult> Add(BillTypeFormViewModel model)
        {
            if (await billTypeService.BillTypeExistsAsync(model, User.Id()))
            {
                ModelState.AddModelError(nameof(model.Name), "Bill Type already exist.");
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await billTypeService.CreateCustomBillTypeAsync(model, User.Id());
            return RedirectToAction("Index");

        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (!(await billTypeService.AllCustomBillTypesAsync(User.Id())).Any(b=>b.Id==id))
            {
                return NotFound();
            }
            await billTypeService.DeleteCustomBillTypeByIdAsync(id);

            return RedirectToAction("Index");
        }
    }
}
