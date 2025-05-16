using Microsoft.AspNetCore.Mvc;

namespace JobBoard.Dtos
{
    public class SavedListingRequest 
    {
        public int CandidateId { get; set; }
        public int ListingId { get; set; }
    }
}
