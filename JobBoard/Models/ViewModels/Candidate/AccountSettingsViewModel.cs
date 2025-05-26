using System.Collections.Generic;
using JobBoard.Security;

namespace JobBoard.Models.ViewModels;
using JobBoard.Models.plainModels;
public class AccountSettingsViewModel
{
    public UserData user { get; set; }
    public int AccountId { get; set; }
    public int CandidateId { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string PhotoPath { get; set; }
    public string ResumePath { get; set; }
    public string ExperienceLevel { get; set; }
    public string Education { get; set; }
    public List<Skills> AllSkills { get; set; }
    public List<int> SelectedSkillIds { get; set; }
    
    public AccountSettingsViewModel() {
    }
}