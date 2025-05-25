using JobBoard.Data;
using JobBoard.Models.plainModels;

namespace JobBoard.Services;

public class ApplyService
{
    private ApplicationDbContext _context;
    public ApplyService(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<bool> SubmitApplication(Candidate candidate, Listing job)
    {
        var application = new Application
        {
            CandidateId = candidate.Id,
            ListingId   = job.Id,
            Status    = ApplicationStatus.PENDING,    
            AppliedAt = DateOnly.FromDateTime(DateTime.Now)          };

        _context.Application.Add(application);
        var saved = await _context.SaveChangesAsync();
        return saved > 0;
    }
}