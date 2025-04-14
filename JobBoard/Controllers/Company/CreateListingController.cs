using Microsoft.AspNetCore.Mvc;

namespace JobBoard.Controllers.Company
{
    public class CreateListingController : Controller
    {
        [HttpGet("/createListing")]

        public IActionResult Index()
        {
            return View("~/Views/Company/CreateListing.cshtml");
        }
    }
}
