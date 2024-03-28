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
            
            var model = await householdService.AllHouseholdMembersAsync(User.Id());

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            if (await householdService.OverMembersLimitAsync(User.Id()))
            {
                return BadRequest();
            }
            var model = new HouseholdMemberFormViewModel();           
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(HouseholdMemberFormViewModel model)
        {
            if(await householdService.OverMembersLimitAsync(User.Id()))
            {
                return BadRequest();
            }
            

            if (!ModelState.IsValid)
            {               
                return View(model);
            }

            await householdService.CreateHouseholdMemberAsync(model, User.Id());
            return RedirectToAction("Index");

        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (await householdService.MemberExistsAsync(id) == false)
            {
                return BadRequest();
            }

            if (await householdService.MemberBelongsToUserAsync(id, User.Id()) == false)
            {
                return Unauthorized();
            }
            await householdService.DeleteHouseholdMemberByIdAsync(id);

            return RedirectToAction("Index");
        }

       
    }
}
