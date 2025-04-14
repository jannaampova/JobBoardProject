using JobBoard.Controllers;
using JobBoard.Data;
using JobBoard.Models.plainModels;
using JobBoard.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace JobBoard.Services
{
    public class FilterService 
    {
        private readonly ApplicationDbContext _context;
        public FilterService(ApplicationDbContext context) { 
        _context = context;
        }

        public List<Industry> getIndustries()
        {
            var industries = _context.Industry
               .Select(i => new Industry
               {
                   Id = i.Id,
                   industry = i.industry,
               })
               .ToList();
            return industries;
        }
        public List<JobType> getJobTypes()
        {

            var jobTypes = _context.JobType
               .Select(jt => new JobType
               {
                   Id = jt.Id,
                   jobType = jt.jobType,
               })
               .ToList();
            return jobTypes;
        }

        public List<Town> getCities()
        {
            List<Town> cities = _context.Town
               .Select(t => new Town
               {
                   Id = t.Id,
                   Name = t.Name,
               })
               .ToList();
            return cities;
        }
        public List<String> getExperienceLevel()
        {
            return Enum.GetNames(typeof(ExperienceLevel)).ToList();
        }
        public List<Listing> GetAllJobListings()
        {
            return _context.Listings
                .Include(l => l.jobType)       
                .Include(l => l.town)         
                .Include(l => l.company)       
                .ThenInclude(c => c.industry) 
                .ToList();                     
        }

        public CandidateDashboardViewModel GetFilteredJobs(CandidateDashboardViewModel filters)
        {
            var listings = _context.Listings
               .Include(l => l.jobType)
               .Include(l => l.town)
               .Include(l => l.company)
               .ThenInclude(c => c.industry)
               .AsQueryable();

            bool hasFilter = false;

            // Apply Search Filter
            if (!string.IsNullOrWhiteSpace(filters.SearchQuery))
            {
                listings = listings.Where(j => j.title.Contains(filters.SearchQuery));
                hasFilter = true;
            }

            // Apply Industry Filter
            if (filters.SelectedIndustries?.Any() == true)
            {
                listings = listings.Where(j => filters.SelectedIndustries.Contains(j.company.industryId));
                hasFilter = true;
            }

            // Apply Job Type Filter
            if (filters.SelectedJobTypes?.Any() == true)
            {
                listings = listings.Where(j => filters.SelectedJobTypes.Contains(j.jobTypeId));
                hasFilter = true;
            }

            // Apply City Filter
            if (filters.SelectedCities?.Any() == true)
            {
                listings = listings.Where(j => filters.SelectedCities.Contains(j.townId));
                hasFilter = true;
            }

            // Apply Experience Level Filter
            if (filters.SelectedLevels?.Any() == true)
            {
                listings = listings.Where(j => filters.SelectedLevels.Contains(j.experienceLevel));
                hasFilter = true;
            }

            // Set the filter data to show in the view
            filters.Industries = getIndustries();
            filters.JobTypes = getJobTypes();
            filters.Cities = getCities();
            filters.Levels = getExperienceLevel();

            // Set the filtered job listings in the view model
            filters.FilteredJobs = listings.ToList();

            return filters;

        }


    }
}
