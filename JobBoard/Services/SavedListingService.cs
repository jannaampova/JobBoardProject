using JobBoard.Data;
using JobBoard.Models.plainModels;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Services;

public class SavedListingService
{
    private readonly ApplicationDbContext _context;

    public SavedListingService(ApplicationDbContext context)
    {
        _context = context;
    }

    public void saveListingToCandidate(int listingId, int candidateId)
    {
        var listings = _context.SavedListings.Where(sl => sl.CandidateId == candidateId);

        bool alreadySaved = listings.Any(sl => sl.ListingId == listingId);
        if (!alreadySaved)
        {
            var newListing = new SavedListings()
            {
                CandidateId = candidateId,
                ListingId = listingId
            };
            _context.SavedListings.Add(newListing);
        }
        else
        {
            var savedListing = listings.FirstOrDefault(sl => sl.ListingId == listingId);
            if (savedListing != null)
            {
                _context.SavedListings.Remove(savedListing);
            }
        }

        _context.SaveChanges();
    }

    public List<Listing> findSavedListingsForCandidate(int candidateId)
    {
        var savedListingIds = _context.SavedListings
            .Where(sl => sl.CandidateId == candidateId)
            .Select(sl => sl.ListingId);
        return _context.Listings
            .Where(l => savedListingIds.Contains(l.Id))
            .Include(l => l.company)
            .ThenInclude(c => c.industry)
            .Include(l => l.town)
            .Include(l => l.jobType)
            .ToList();
 }
    
}