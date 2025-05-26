using JobBoard.Models.plainModels;
using JobBoard.Security;

namespace JobBoard.Models.ViewModels;

public class EditListingViewModel
{
    public List<string> selectedRequirements{get;set;}
    public List<string> selectedBenefits{get;set;}
    public int selectedTownId {get; set;}
    public int selectedJobType {get;set;}
    public DateOnly datePosted {get; set;}
    public UserData user { get; set; }
    public List<String> requirements { get; set; }
    public List<String> benefits { get; set; }
    public List<JobType> jobTypes { get; set; }
    public Listing selectedListing { get; set; }
    public List<Industry> industries { get; set; }
    public List<ExperienceLevel> experienceLevels { get; set; }
    public String selectedExperienceLevel { get; set; }
    public List<Town> towns { get; set; }
    public string Title { get; set; }
    public string Desc{get;set;}
}