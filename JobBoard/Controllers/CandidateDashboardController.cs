using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobBoard.Data; // or your actual namespace
using JobBoard.Models;
using JobBoard.Services;
using JobBoard.Migrations;
using JobBoard.Models.ViewModels;
using JobBoard.Security;
using Microsoft.AspNetCore.Identity;

namespace JobBoard.Controllers
{

    public class CandidateDashboardController : Controller
    {
        private readonly FilterService filterService;
        private readonly UserManager<UserData> _userManager;



        public CandidateDashboardController(FilterService filters, UserManager<UserData> userManager)
        {
            filterService = filters;
            _userManager = userManager;
        }
        [HttpGet("applicantDashboard")]

        public async Task<IActionResult> Applicant()
        {

            List<Industry> industries = filterService.getIndustries();
            List<JobType> jobTypes = filterService.getJobTypes();
            UserData currUser =await _userManager.GetUserAsync(User);

            CandidateDashboardViewModel viewModel = new CandidateDashboardViewModel
            {
                Industries = industries,
                JobTypes = jobTypes,
                user = currUser
            };

            return View("~/Views/Dashboards/CandidateDashboard.cshtml",viewModel);
        }

     
    }
}
