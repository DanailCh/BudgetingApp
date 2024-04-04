using App.Core.Contracts;
using App.Core.Models.FeedbackMessage;
using App.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static App.Core.Constants.FeedbackMessageCommentConstants;

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
            TempData["Message"] = ConfirmSendComment;

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            if (await _messageService.MessageExistsAsync(id) == false)
            {
                return BadRequest();
            }

            if (await _messageService.MessageBelongsToUser(id, User.Id()) == false)
            {
                return Unauthorized();
            }
            await _messageService.RemoveMessageAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
