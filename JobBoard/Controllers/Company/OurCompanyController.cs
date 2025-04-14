using Microsoft.AspNetCore.Mvc;

namespace JobBoard.Controllers.Company
{
    public class OurCompanyController : Controller
    {
        [HttpGet("/ourCompany")]

        public IActionResult Index()
        {
            return View("~/Views/Company/OurCompany.cshtml");
        }
    }
}
