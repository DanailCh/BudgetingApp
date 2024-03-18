using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HouseholdBudgetingApp.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {

    }
}
