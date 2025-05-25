using JobBoard.Models.plainModels;
using JobBoard.Security;

namespace JobBoard.Models.ViewModels;

public class ApplicationsViewModel
{
    public UserData user { get; set; }
    public int count { get; set; }

    public List<Application> applications { get; set; }
    public int candidateId { get; set; }

}