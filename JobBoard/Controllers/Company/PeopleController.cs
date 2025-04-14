using Microsoft.AspNetCore.Mvc;

namespace JobBoard.Controllers.Company
{
    public class PeopleController : Controller
    {
        [HttpGet("/people")]

        public IActionResult Index()
        {
            return View("~/Views/Company/People.cshtml");
        }

        [HttpGet("/candidateProfile")]

        public IActionResult ViewProfile()
        {
            return View("~/Views/Company/CandidateProfile.cshtml");
        }
    }
}
