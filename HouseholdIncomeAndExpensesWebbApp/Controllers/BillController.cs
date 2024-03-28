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
            var model = await billService.AllCurentMonthBillsAsync(User.Id(), billService.GetDate());
            foreach (var item in model) { item.Payers = await householdService.GetHouseholdMembersAsync(User.Id()); }
            ViewBag.Date = billService.GetFormatedDate();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
                        
            ViewBag.Date = billService.GetFormatedDate();
            var model = new BillFormModel()
            {
                BillTypes = await billService.GetBillTypesAsync(User.Id()),
                Payers=await householdService.GetHouseholdMembersAsync(User.Id()),                
            };
            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult>Add(BillFormModel model)
        {
            if (string.IsNullOrEmpty(Request.Form["PayerId"]))
            {
                model.IsPayed = false;
                model.PayerId = null;
            }
            else
            {
                model.IsPayed = true;
                model.PayerId = int.Parse(Request.Form["PayerId"]);
            }

            if (!(await billService.GetBillTypesAsync(User.Id())).Any(b => b.Id == model.BillTypeId))
            {
                ModelState.AddModelError(nameof(model.BillTypeId), "Bill Type does not exist.");
            }
            

            if (!ModelState.IsValid)
            {
                model.BillTypes = await billService.GetBillTypesAsync(User.Id());
                model.Payers = await householdService.GetHouseholdMembersAsync(User.Id());

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
                return BadRequest();
            }
            if (await billService.BillIsPayedAsync(id))
            {
                return BadRequest();
            }

            if (await billService.BillBelongsToUserAsync(id, User.Id()) == false)
            {
                return Unauthorized();
            }

            var model=await billService.FindBillByIdAsync(id);
            model.BillTypes = await billService.GetBillTypesAsync(User.Id());            
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(BillFormModel model,int id)
        {
            if (await billService.BillExistsAsync(id) == false)
            {
                return BadRequest();
            }
            if (await billService.BillIsPayedAsync(id))
            {
                return BadRequest();
            }

            if (await billService.BillBelongsToUserAsync(id, User.Id()) == false)
            {
                return Unauthorized();
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
                return BadRequest();
            }

            if (await billService.BillBelongsToUserAsync(id, User.Id()) == false)
            {
                return Unauthorized();
            }
           

            if (!(await householdService.GetHouseholdMembersAsync(User.Id())).Any(m => m.Id == model.PayerId))
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
                return BadRequest();
            }

            if (await billService.BillBelongsToUserAsync(id, User.Id()) == false)
            {
                return Unauthorized();
            }
            await billService.DeleteBillByIdAsync(id);

            return RedirectToAction("Index");
        }
    }
}
