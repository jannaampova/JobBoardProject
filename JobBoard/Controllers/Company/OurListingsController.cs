using Microsoft.AspNetCore.Mvc;

namespace JobBoard.Controllers.Company
{
    public class OurListingsController : Controller
    {
        [HttpGet("/ourListings")]

        public IActionResult Index()
        {
            return View("~/Views/Company/OurListings.cshtml");
        }
        [HttpGet("/editListing")]

        public IActionResult EditListing()
        {
            return View("~/Views/Company/EditListing.cshtml");
        }
        [HttpGet("/listingDetails")]

        public IActionResult ViewListing()
        {
            return View("~/Views/Company/ListingDetails.cshtml");
        }
    }
}
