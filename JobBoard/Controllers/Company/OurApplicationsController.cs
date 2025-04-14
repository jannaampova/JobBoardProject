using Microsoft.AspNetCore.Mvc;

namespace JobBoard.Controllers.Company
{
    public class OurApplicationsController : Controller
    {
        [HttpGet("/ourApplications")]

        public IActionResult Index()
        {
            return View("~/Views/Company/OurApplications.cshtml");
        }

        [HttpGet("/Application")]

        public IActionResult ViewApplication()
        {
            return View("~/Views/Company/ApplicationDetails.cshtml");
        }
    }
}
