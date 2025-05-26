using JobBoard.Models.plainModels;
using JobBoard.Security;
using Microsoft.AspNetCore.Mvc;

namespace JobBoard.Models.ViewModels

{
    public class CompanyDashboardViewModel
    {
        public UserData user { get; set; }
        public int PendingReviews { get; set; }
        public int TotalApplications { get; set; }
        public int ActiveListings { get; set; }

    }
}
