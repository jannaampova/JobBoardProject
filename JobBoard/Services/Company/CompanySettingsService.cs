using JobBoard.Data;
using JobBoard.Models.plainModels;
using Microsoft.Identity.Client;

namespace JobBoard.Services.Company;

public class CompanySettingsService
{
    private ApplicationDbContext _context;
    public CompanySettingsService(ApplicationDbContext context)
    {
        _context = context;
    }

    public void ChangeDetails( int CompanyId,int AccountId,
        string Name,
        string Industry,
        string Description,
        string PhotoUrl)
    {
        var company = _context.Company.FirstOrDefault(c => c.Id == CompanyId);
        var account = _context.Account.FirstOrDefault(a => a.Id == AccountId);
        account.Name = Name;
        company.industry    = _context.Industry.Single(i => i.industry == Industry);
        company.description = Description;
        if (!string.IsNullOrWhiteSpace(PhotoUrl))
            company.photoPath = PhotoUrl;
        _context.SaveChanges();
   }

    public void ChangeAccount(int companyId, int accountId, string email, string phone, string webUrl)
    {
        var company = _context.Company.FirstOrDefault(c => c.Id == companyId);
        var account = _context.Account.FirstOrDefault(a => a.Id == accountId);
        account.Email = email;
        account.Phone = phone;
        company.webUrl = webUrl;
        _context.SaveChanges();
    }
}