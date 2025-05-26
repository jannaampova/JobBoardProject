using JobBoard.Data;
using JobBoard.Models.plainModels;
using JobBoard.Security;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Services.Company;

public class CompanyApplicationsService
{
    private readonly ApplicationDbContext _context;

    public CompanyApplicationsService(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<Application> FindCompanyApplications(UserData user)
    {
        var account = _context.Account.FirstOrDefault(a => a.UserId == user.Id);
        if (account == null) return new List<Application>();

        var company = _context.Company.FirstOrDefault(c => c.accountId == account.Id);
        if (company == null) return new List<Application>();

        var applications = _context.Application
            .Include(a => a.Candidate)
            .ThenInclude(c => c.Account)     
            .Include(a => a.Listing)
            .ThenInclude(l => l.company)
            .Where(a => a.Listing.companyId == company.Id)
            .ToList();

        return applications;
    }

    public async Task<Application> fetchApplicationInfo(int applicationId)
    {
        var application = await _context.Application
            .Include(a => a.Candidate)
            .ThenInclude(c => c.Account)
            .Include(a => a.Listing)
            .ThenInclude(l => l.town)
            .FirstOrDefaultAsync(a => a.Id == applicationId);

        if (application?.Candidate != null)
        {
            // Manually fetch candidate skills
            var skills = await _context.CandidateSkills
                .Where(cs => cs.CandidateId == application.Candidate.Id)
                .Include(cs => cs.Skill)
                .ToListAsync();

            application.Candidate.candidateSkills = skills;
        }

        return application;
    }

    public void changeStatus(int id, ApplicationStatus status)
    {
        
        var application = _context.Application.FirstOrDefault(a => a.Id == id);
        if (application == null) return;
        application.Status = status;
        _context.Update(application);
        _context.SaveChanges();
    }
}