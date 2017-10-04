using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EMS2.ViewModel;
using Microsoft.EntityFrameworkCore;
using EMS2.Models;


namespace EMS2.Controllers
{
    [Produces("application/json")]
    [Route("api/Employee")]
    public class PeopleController : Controller
    {
        private readonly AdventureWorks2014Context _context;

        public PeopleController(AdventureWorks2014Context context)
        {
            _context = context;            
        }
        [HttpGet]
        public async Task<IActionResult> EmployeeList()
        {
            List<PersonEmployee> ilIst = new List<PersonEmployee>();
            var listData = await (from per in _context.Person 
                                  join emp in _context.Employee on per.BusinessEntityId equals emp.BusinessEntityId
                                  join pp in _context.PersonPhone on per.BusinessEntityId equals pp.BusinessEntityId
                                  select new
                                  {
                                      per.BusinessEntityId,
                                      per.LastName,
                                      per.FirstName,
                                      emp.JobTitle,
                                      per.EmailAddress,
                                      pp.PhoneNumber
                                  }
                          ).ToListAsync();
            listData.ForEach(x =>
            {
                PersonEmployee Obj = new PersonEmployee();
                Obj.BusinessEntityID = x.BusinessEntityId;
                Obj.LastName = x.LastName;
                Obj.FirstName = x.FirstName;
                Obj.JobTitle = x.JobTitle;
                Obj.EmailAddress = x.JobTitle;
                Obj.PhoneNumber = x.PhoneNumber;
                ilIst.Add(Obj);
            });

            return Json(ilIst);
        }
    }
}