using Microsoft.AspNetCore.Mvc;

namespace JobBoard.Controllers
{
    public class CompanyDashboardController : Controller
    {
        public IActionResult Company()
        {
            return View("~/Views/Dashboards/CompanyDashboard.cshtml");
        }
    }
}
