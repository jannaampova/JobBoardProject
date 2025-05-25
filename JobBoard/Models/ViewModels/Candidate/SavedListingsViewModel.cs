using JobBoard.Models.plainModels;
using JobBoard.Security;

namespace JobBoard.Models.ViewModels;

public class SavedListingsViewModel
{
    public UserData user { get; set; }
    public List<Listing> savedListings { get; set; }
    public int candidateId { get; set; }

     
    public SavedListingsViewModel() {
        
    }
}