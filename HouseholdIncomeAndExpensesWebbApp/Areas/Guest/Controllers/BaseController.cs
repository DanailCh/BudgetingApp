using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HouseholdBudgetingApp.Areas.Guest.Controllers
{
    [Authorize(Roles = "Guest")]
    [Area("Guest")]
    public class BaseController : Controller
    {

    }
}
