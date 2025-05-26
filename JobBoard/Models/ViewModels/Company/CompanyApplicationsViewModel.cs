using Azure.Core.Pipeline;
using JobBoard.Models.plainModels;
using JobBoard.Security;

namespace JobBoard.Models.ViewModels;

public class CompanyApplicationsViewModel

{
    public UserData user { get; set; }
    public List<Application> Applications { get; set; }
    public Application application { get; set; }
    public List<CandidateSkills> candidateSkills { get; set; }
    public ApplicationStatus? SelectedStatus { get; set; }
}