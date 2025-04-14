using JobBoard.Models.plainModels;
using JobBoard.Security;
using Microsoft.AspNetCore.Mvc;

namespace JobBoard.Models.ViewModels
{
    public class CandidateDashboardViewModel 
    {
        public CandidateDashboardViewModel()
        {
            SelectedIndustries = new List<int>();
            SelectedJobTypes = new List<int>();
            SelectedCities = new List<int>();
            SelectedLevels = new List<string>();
        }
        //info bubbles
        public List<Industry> Industries { get; set; }
        public List<JobType> JobTypes { get; set; }
        public List<Town> Cities { get; set; }
        public List<String> Levels { get; set; }
        //user data
        public UserData user { get; set; }

        //filters
        public List<int> SelectedIndustries { get; set; }
        public List<int> SelectedJobTypes { get; set; }
        public List<int> SelectedCities { get; set; }
        public List<string> SelectedLevels { get; set; }
        public string SearchQuery { get; set; }
        public List<Listing> FilteredJobs { get; set; }
        public List<Listing> AllListings { get; set; }
        public bool IsFiltered { get; set; } = false;


    }
}
