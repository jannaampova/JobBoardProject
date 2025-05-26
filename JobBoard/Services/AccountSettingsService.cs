using JobBoard.Data;
using JobBoard.Models.plainModels;

namespace JobBoard.Services;

public class AccountSettingsService
{
    private ApplicationDbContext _context;
    public AccountSettingsService(ApplicationDbContext context)
    {
        _context = context;
    }

    public void UpdateSkills(int candidateId, List<int> selectedSkillIds)
    {
        var existing = _context.CandidateSkills
            .Where(cs => cs.CandidateId == candidateId);
        _context.CandidateSkills.RemoveRange(existing);

        var toAdd = selectedSkillIds.Select(skillId => new CandidateSkills()
        {
            CandidateId = candidateId,
            SkillId     = skillId
        });
        _context.CandidateSkills.AddRange(toAdd);

        _context.SaveChanges();    }

    public void UpdatePhotoPath(int candidateId, string imageUrl)
    {
        var candidate = _context.Candidate.Find(candidateId);
        if (candidate == null)
            throw new InvalidOperationException($"Candidate {candidateId} not found.");

        candidate.PhotoPath = imageUrl;
        _context.SaveChanges();    
    }

    public void UpdateResumePath(int candidateId, string resumeUrl)
    {
        var candidate = _context.Candidate.Find(candidateId);
        if (candidate == null)
            throw new InvalidOperationException($"Candidate {candidateId} not found.");

        candidate.ResumePath = resumeUrl;
        _context.SaveChanges();
        
    }

    public void ChangeDetails(int candidateId, string fullName, string email, string phone, ExperienceLevel level, string education)
    {
        var candidate = _context.Candidate.Find(candidateId);
        if (candidate == null)
            throw new InvalidOperationException($"Candidate {candidateId} not found.");

        var account = _context.Account.Find(candidate.AccountId);
        if (account == null)
            throw new InvalidOperationException($"Account for candidate {candidateId} not found.");

        account.Name  = fullName;
        account.Email = email;
        account.Phone = phone;
        candidate.ExperienceLevel = level.ToString();
        candidate.Education = education;

        _context.SaveChanges();
    }

}