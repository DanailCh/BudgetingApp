using App.Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using App.Core.Models.FeedbackMessage;
using System.Security.Claims;

namespace HouseholdBudgetingApp.Areas.Admin.Controllers
{
    public class MessagesController : AdminBaseController
    {
        private readonly IFeedBackMessageService feedBackMessageService;
        public MessagesController(IFeedBackMessageService _feedBackMessageService)
        {
            feedBackMessageService = _feedBackMessageService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await feedBackMessageService.AdminGetAllMessagesAsync();
            return View(model);
        }
    }
}
