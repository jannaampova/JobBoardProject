using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobBoard.Data; // or your actual namespace
using JobBoard.Models;
using JobBoard.Services;
using JobBoard.Migrations;
using JobBoard.Models.ViewModels;

namespace JobBoard.Controllers
{

    public class CandidateDashboardController : Controller
    {
        private readonly FilterService filterService;


        public CandidateDashboardController(FilterService filters)
        {
            filterService = filters;
        }
        [HttpGet("applicantDashboard")]

        public IActionResult Applicant()
        {

            List<Industry> industries = filterService.getIndustries();
            List<JobType> jobTypes = filterService.getJobTypes();

            CandidateDashboardViewModel viewModel = new CandidateDashboardViewModel
            {
                Industries = industries,
                JobTypes = jobTypes
            };

            return View("~/Views/Dashboards/CandidateDashboard.cshtml",viewModel);
        }

     
    }
}
