using JobBoard.Models.plainModels;
using JobBoard.Security;

namespace JobBoard.Models.ViewModels;

public class CompanySettingsViewModel
{
    public UserData user { get; set; }
    public int AccountId { get; set; }
    public int CompanyId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string PhotoPath { get; set; }
    public List<Industry> AllIndustries { get; set; }
    public string Industry { get; set; }
    public string WebURL { get; set; }
    public string Description { get; set; }
    public string PhotoUrl { get; set; }
    public CompanySettingsViewModel() {
    }
}