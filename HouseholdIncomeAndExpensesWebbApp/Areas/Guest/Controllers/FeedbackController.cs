using App.Core.Contracts;
using App.Core.Models.FeedbackMessage;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HouseholdBudgetingApp.Areas.Guest.Controllers
{

    public class FeedbackController : BaseController
    {
        private readonly IFeedBackMessageService _messageService;

        public FeedbackController(IFeedBackMessageService messageService)
        {
            _messageService = messageService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
           
            var model = new GuestFeedbackMessageFormViewModel();
            model.FeedbackMessages= await _messageService.GetAllMessagesAsync(User.Id());
            return View(model);
        }

        

        [HttpPost]
      
        public async Task<IActionResult> Add(FeedbackMessageFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
           await _messageService.CreateMessageAsync(model,User.Id());

            return RedirectToAction(nameof(Index));
        }
    }
}
