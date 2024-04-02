using App.Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using App.Core.Models.FeedbackMessage;
using System.Security.Claims;
using App.Core.Services;
using App.Core.Models.Archive.Bill;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace HouseholdBudgetingApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class MessagesController : Controller
    {
        private readonly IFeedBackMessageService feedBackMessageService;
        public MessagesController(IFeedBackMessageService _feedBackMessageService)
        {
            feedBackMessageService = _feedBackMessageService;
        }
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] AllFeedbackQueryModel model)
        {
            var messages = await feedBackMessageService.AdminGetAllMessagesAsync(
                User.Id(),model
            );
            model.SeverityTypes = await feedBackMessageService.GetSeverityTypesAsync();
            model.Statuses = await feedBackMessageService.GetStatusesAsync();
            model.MessagesCount = messages.MessagesCount;
            model.Messages = messages.Messages;           
            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AsignPriority(int id,int severityId)
        {
            await feedBackMessageService.SetSeverityStatusOnMessageAsync(id, severityId);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Complete(int id)
        {
            await feedBackMessageService.SetDoneStatusOnMessageAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
