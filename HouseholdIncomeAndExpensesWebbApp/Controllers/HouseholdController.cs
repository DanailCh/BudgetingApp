using App.Core.Contracts;
using App.Core.Models.Bill;
using App.Core.Models.HouseholdMember;
using App.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Security.Claims;

namespace HouseholdBudgetingApp.Controllers
{
    public class HouseholdController:BaseController
    {
        private readonly IHouseholdService householdService;

        public HouseholdController(
            IHouseholdService _householdService)
        {
           
            householdService = _householdService;

        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = User.Id();
            var model = await householdService.AllHouseholdMembersAsync(userId);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new HouseholdMemberFormViewModel();           
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(HouseholdMemberFormViewModel model)
        {
            var userId = User.Id();
            

            if (!ModelState.IsValid)
            {               
                return View(model);
            }

            await householdService.CreateHouseholdMemberAsync(model, userId);
            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {           
            var model = await householdService.FindHouseholdMemberByIdAsync(id);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(HouseholdMemberFormViewModel model,int id)
        {
            
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await householdService.EditHouseholdMemberByIdAsync(model, id);
            return RedirectToAction("Index");

        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await householdService.DeleteHouseholdMemberByIdAsync(id);

            return RedirectToAction("Index");
        }

       
    }
}
