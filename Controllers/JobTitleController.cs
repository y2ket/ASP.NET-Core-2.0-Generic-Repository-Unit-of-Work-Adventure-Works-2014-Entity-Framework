using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EMS2.Models;
using Microsoft.EntityFrameworkCore;
using EMS2.ViewModel;

namespace EMS.Controllers
{
    [Produces("application/json")]
    [Route("api/jobtitles")]
    public class JobTitleController : Controller
    {
        private readonly AdventureWorks2014Context _context;

        public JobTitleController(AdventureWorks2014Context context)
        {
            _context = context;
        }
        // GET: api/Projects  
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<JobTitles> jobTitles = new  List<JobTitles>();
            var Job = await (from data in _context.Employee
                                 select new
                                 {
                                     JobTitle = data.JobTitle,
                                     //BusinessEntityId = data.BusinessEntityId                                     
                                 }).Distinct().ToListAsync();
            int i = 1;
            Job.ForEach(x =>
            {                
                JobTitles jobs = new JobTitles();                
                jobs.JobTitle = x.JobTitle;
                jobs.JobTitleId = i;
                jobTitles.Add(jobs);
                i++;
            });
             
            return Json(jobTitles);
        }



    }
}