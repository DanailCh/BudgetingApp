using App.Core.Contracts;
using App.Core.Models.FeedbackMessage;
using HouseholdBudgetingApp.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HouseholdBudgetingApp.Controllers
{
    public class FeedbackController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly IFeedBackMessageService _messageService;
        public FeedbackController(ApplicationDbContext context,IFeedBackMessageService messageService)
        {
            _context = context;
            _messageService = messageService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<FeedbackMessageViewModel> model;
            if (User.IsAdmin())
            {
                 model = await _messageService.AdminGetAllMessagesAsync();
            }
            else
            {
                model = await _messageService.GetAllMessagesAsync(User.Id());
            }
             
            return View(model);
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add()
        {
            FeedbackMessageFormModel model= new FeedbackMessageFormModel();
            
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(FeedbackMessageFormModel model)
        {
            //FeedbackMessageFormModel model = new FeedbackMessageFormModel();
            //if (User.IsAdmin())
            //{
            //    model.SeverityTypes = await _messageService.GetSeverityTypesAsync();
            //}

            return View(model);
        }
    }
}
