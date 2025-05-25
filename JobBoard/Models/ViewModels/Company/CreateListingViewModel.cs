using JobBoard.Models.plainModels;
using JobBoard.Security;

namespace JobBoard.Models.ViewModels;

public class CreateListingViewModel
{
    public UserData user { get; set; }
    public int CompanyId { get; set; }
    public string Industry { get; set; }
    public List<JobType> jobTypes { get; set; }
    public List<Town> cities { get; set; }
    public List<ExperienceLevel> levels { get; set; }
    public List<Benefits> Benefits { get; set; }
    public List<Requirements> Requirements { get; set; }
    
    public string Title { get; set; }
    public string Description { get; set; }
    public int JobTypeId { get; set; }
    public int TownId { get; set; }
    public string Level { get; set; }
    public List<int> RequirementIds { get; set; } = new();
    public List<int> BenefitIds     { get; set; } = new();
    public DateOnly DatePosted { get; set; }
    public CreateListingViewModel() {
    }

}