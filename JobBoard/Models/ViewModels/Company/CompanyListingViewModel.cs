using JobBoard.Models.plainModels;
using JobBoard.Security;

namespace JobBoard.Models.ViewModels;

public class CompanyListingViewModel
{
    public UserData user { get; set; }
    public List<Listing> listingsByCompany { get; set; }
    public List<String> requirements { get; set; }
    public List<String> benefits { get; set; }
    public int applicationsCounter { get; set; }
    public Listing selectedListing { get; set; }
    public List<Industry> industries { get; set; }
    

}