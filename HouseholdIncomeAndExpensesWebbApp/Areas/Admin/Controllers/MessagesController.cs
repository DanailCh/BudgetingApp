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
    [Authorize(Roles = "Administrator,MasterAdmin")]
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
            var messages = await feedBackMessageService.AdminGetAllMessagesAsync(model);
            model.SeverityTypes = await feedBackMessageService.GetSeverityTypesAsync();
            model.Statuses = await feedBackMessageService.GetStatusesAsync();
            model.MessagesCount = messages.MessagesCount;
            model.Messages = messages.Messages;           
            
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AsignPriority(int messageId,int severityId, AllFeedbackQueryModel model)
        {
            if (!(await feedBackMessageService.MessageExistsAsync(messageId)))
            {
                return NotFound();
            }
            if (!(await feedBackMessageService.SeverityTypeExistsAsync(severityId)))
            {
                return NotFound();
            }
            await feedBackMessageService.SetSeverityTypeOnMessageAsync(messageId, severityId);
            return RedirectToAction(nameof(Index),model);
        }
        [HttpGet]
        public async Task<IActionResult> Complete(int messageId, AllFeedbackQueryModel model)
        {
            if (!(await feedBackMessageService.MessageExistsAsync(messageId)))
            {
                return NotFound();
            }

            await feedBackMessageService.SetDoneStatusOnMessageAsync(messageId);
            return RedirectToAction(nameof(Index),model);
        }
    }
}
