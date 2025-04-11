using JobBoard.Security;
using Microsoft.AspNetCore.Mvc;

namespace JobBoard.Models.ViewModels
{
    public class CandidateDashboardViewModel : Controller
    {
        public List<Industry> Industries { get; set; }
        public List<JobType> JobTypes { get; set; }
        public UserData user { get; set; }
    }
}
