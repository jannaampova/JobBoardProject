using JobBoard.Models.plainModels;
using JobBoard.Security;

namespace JobBoard.Models.ViewModels;

public class CompanyApplications

{
    public UserData user { get; set; }
    public List<Application> Applications { get; set; }
    public Application application { get; set; }
}