using JobBoard.Data;
using JobBoard.Models.plainModels;
using JobBoard.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Services
{
    public class SavedListingService
    {
        private readonly ApplicationDbContext _context;  // Changed to ApplicationIdentityDbContext
        private readonly UserManager<UserData> _userManager;

        public SavedListingService(ApplicationDbContext context, UserManager<UserData> userManager, SignInManager<UserData> signInManager)
        {
            
            _context = context;
            _userManager = userManager;
        }
        internal void SaveListing(UserData? currUser, int listingId)
        {
            if (currUser == null)
            {
                throw new InvalidOperationException("User is not logged in.");
            }

            var listing = _context.Listings.FirstOrDefault(l => l.Id == listingId);
            if (listing == null)
            {
                throw new ArgumentException("Listing not found.");
            }

            if (int.TryParse(currUser.Id, out int userIdInt))
            {
                var existingSavedListing = _context.SavedListings
                    .FirstOrDefault(sl => sl.candidateId == userIdInt && sl.listingId == listingId);

                if (existingSavedListing == null)
                {
                    var newSavedListing = new SavedListings
                    {
                        candidateId = userIdInt,
                        listingId = listingId
                    };

                    _context.SavedListings.Add(newSavedListing);
                    _context.SaveChanges();
                }
            }
            else
            {
                throw new InvalidOperationException("Invalid user ID format.");
            }



  
        }

    }
}
