using JobBoard.Models.plainModels;
using JobBoard.Security;

namespace JobBoard.Models.ViewModels;

public class PeopleViewModel
{
    public UserData user { get; set; }
    public int CandidateId { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string PhotoPath { get; set; }
    public string ResumePath { get; set; }
    public string ExperienceLevel { get; set; }
    public string Education { get; set; }
    public List<Skills> Skills { get; set; }
    public List<Candidate> Candidates { get; set; }

    public PeopleViewModel() {
    }
}