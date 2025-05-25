using JobBoard.Models.plainModels;
using JobBoard.Security;

namespace JobBoard.Models.ViewModels
{
    public class JobDetailsModel
    {
        public UserData user { get; set; }
        public Listing jobListed { get; set; }
        public List<string> requirements { get; set; } 
        public List<string> benefits { get; set; } 
        public bool IsSaved { get; set; }
        public JobDetailsModel() {
        }

    }
}
