using App.Core.Contracts;
using App.Core.Models.HouseholdMember;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static App.Core.Constants.TempDataMessagesConstants;
using static App.Core.Constants.TempDataErrorMessagesConstants;

namespace HouseholdBudgetingApp.Areas.Guest.Controllers
{

    public class HouseholdController : BaseController
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
            var model = new HouseholdMemberFormViewModel();
            model.Members = await householdService.AllHouseholdMembersAsync(User.Id());

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(HouseholdMemberFormViewModel model)
        {
            if (await householdService.OverMembersLimitAsync(User.Id()))
            {
                TempData["ErrorMessage"] = OverMemberLimitMessage;
                return BadRequest();
            }


            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await householdService.CreateHouseholdMemberAsync(model, User.Id());
            TempData["Message"] =HouseholdMemberAddedMessage;
            return RedirectToAction("Index");

        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (!(await householdService.AllHouseholdMembersAsync(User.Id())).Any(b => b.Id == id))
            {
                return NotFound();
            }
            
            await householdService.DeleteHouseholdMemberByIdAsync(id);

            return RedirectToAction("Index");
        }


    }
}
