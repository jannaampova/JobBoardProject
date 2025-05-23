namespace JobBoard.Models.plainModels;

public class SavedListings
{
    public int CandidateId { get; set; }
    public Candidate candidate { get; set; }
    public int ListingId { get; set; }
    public Listing listing { get; set; }
    
}