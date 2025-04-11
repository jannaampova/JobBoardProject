using JobBoard.Controllers;
using JobBoard.Data;
using JobBoard.Models;
using JobBoard.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace JobBoard.Services
{
    public class FilterService 
    {
        private readonly ApplicationDbContext _context;
        public FilterService(ApplicationDbContext context) { 
        _context = context;
        }


       public  List<Industry> getIndustries()
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


    }
}
