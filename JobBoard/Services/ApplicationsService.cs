using JobBoard.Data;
using JobBoard.Models.plainModels;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Services;

public class ApplicationsService
{
    private readonly ApplicationDbContext _context;

    public ApplicationsService(ApplicationDbContext context)
    {
        _context = context;
    }
    public List<Application> FindCandidateApplications(int candidateId)
    {
        return _context.Application
            .Where(a => a.CandidateId == candidateId)
            .Include(a => a.Listing)
            .ThenInclude(l => l.company)
            .ThenInclude(c => c.industry)
            .Include(a => a.Listing)
            .ThenInclude(l => l.town)
            .Include(a => a.Listing)
            .ThenInclude(l => l.jobType)           
            .OrderByDescending(a => a.AppliedAt)
            .ToList();
    }
}