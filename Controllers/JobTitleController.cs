using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EMS2.Models;
using Microsoft.EntityFrameworkCore;
using EMS2.ViewModel;
using EMS2.Repository;
namespace EMS.Controllers
{
    [Produces("application/json")]
    [Route("api/jobtitles")]
    public class JobTitleController : Controller
    {
        private readonly AdventureWorks2014Context _context;
        private UnitOfWork _unitOfWork;
        IGenericRepository<Employee> _db;

      
  
        public JobTitleController(AdventureWorks2014Context context)
        {
            _context = context;
            _unitOfWork = new UnitOfWork(_context);
            _db = _unitOfWork.GenericRepository<Employee>();
       

        }
        // GET: api/Projects 

        #region Angular
        [HttpGet]
        //public async Task<IActionResult> Get()
        //{
        //    List<JobTitles> jobTitles = new List<JobTitles>();
        //    var Job = await (from data in _context.Employee
        //                     select new
        //                     {
        //                         JobTitle = data.JobTitle
        //                         //BusinessEntityId = data.BusinessEntityId                                     
        //                     }).Distinct().ToListAsync();
        //    int i = 1;
        //    Job.ForEach(x =>
        //    {
        //        JobTitles jobs = new JobTitles();
        //        jobs.JobTitle = x.JobTitle;
        //        jobs.JobTitleId = i;
        //        jobTitles.Add(jobs);
        //        i++;
        //    });

        //    return Json(jobTitles);
        //}
        #endregion

        [HttpGet]
        public ActionResult Read(int page = 1)
        {
            int pageSize = 20;

            List<Employee> employees = null;
            List<JobTitles> jobTitles = new List<JobTitles>();
          


            using (BussinessTransaction bt = new BussinessTransaction(_unitOfWork))
            {
                bt.Execute(() =>
                {
                    employees = _db.Read().ToList();
                });
            }

            IEnumerable<string>jobTitles_ = employees.Select(x => x.JobTitle).Distinct().OrderBy(x => x);

            for (int i = 0; i < jobTitles_.Count(); i++)
            {
                JobTitles y = new JobTitles();
                y.JobTitleId = i + 1;
                y.JobTitle = jobTitles_.ElementAt(i);
                jobTitles.Add(y);
            }
            /*
              Console.WriteLine("\nFind: Part where name contains \"seat\": {0}", 
            parts.Find(x => x.PartName.Contains("seat")));
             */



            //var count = jobTitles.Count();     
            //var items = jobTitles.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            //JobTitlesViewModel employee = new JobTitlesViewModel(count, page, pageSize);
            //IndexJobTitle viewModel = new IndexJobTitle
            //{
            //    JobTitlesViewModel_ = employee,
            //    JobTitles_ = items
            //};
            //return View(viewModel);

            return View(jobTitles);
        }


    }
}