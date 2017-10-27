using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EMS2.ViewModel;
using Microsoft.EntityFrameworkCore;
using EMS2.Models;
using System.Data.SqlClient;
using EMS2.Repository;
using System.Data;

namespace EMS2.Controllers
{
    [Produces("application/json")]
    [Route("api/Employee")]
    public class PeopleController : Controller
    {
        private readonly AdventureWorks2014Context _context;
        private UnitOfWork _unitOfWork;

        IGenericRepository<Employee> _dbEmp;
        IGenericRepository<Person> _dbPer;
        IGenericRepository<EmailAddress> _dbEml;
        IGenericRepository<PersonPhone> _dbPhn;
        IGenericRepository<BusinessEntity> _dbBis;




        public PeopleController(AdventureWorks2014Context context)
        {
            _context = context;
            _unitOfWork = new UnitOfWork(_context);
            
            _dbEmp = _unitOfWork.GenericRepository<Employee>();
            _dbPer = _unitOfWork.GenericRepository<Person>();
            _dbPhn = _unitOfWork.GenericRepository<PersonPhone>();
            _dbEml = _unitOfWork.GenericRepository<EmailAddress>();
            _dbBis = _unitOfWork.GenericRepository<BusinessEntity>();
        }
        #region Angular
        //[HttpGet]
        //public async Task<IActionResult> EmployeeList()
        //{
        //    List<PersonEmployee> ilIst = new List<PersonEmployee>();
        //    var listData = await (from per in _context.Person 
        //                          join emp in _context.Employee on per.BusinessEntityId equals emp.BusinessEntityId
        //                          join pp in _context.PersonPhone on per.BusinessEntityId equals pp.BusinessEntityId
        //                          join ea in _context.EmailAddress on per.BusinessEntityId equals ea.BusinessEntityId                                  
        //                          select new
        //                          {
        //                              per.BusinessEntityId,
        //                              per.LastName,
        //                              per.FirstName,
        //                              emp.JobTitle,
        //                              ea.EmailAddress1,
        //                              pp.PhoneNumber,
        //                              emp.CurrentFlag
        //                          }
        //                  ).ToListAsync();
        //    listData.ForEach(x =>
        //    {
        //    if (x.CurrentFlag == true)
        //    {
        //        PersonEmployee Obj = new PersonEmployee();
        //            Obj.BusinessEntityID = x.BusinessEntityId;
        //            Obj.LastName = x.LastName;
        //            Obj.FirstName = x.FirstName;
        //            Obj.JobTitle = x.JobTitle;
        //            Obj.EmailAddress = x.EmailAddress1;
        //            Obj.PhoneNumber = x.PhoneNumber;
        //            ilIst.Add(Obj);
        //        }
        //    });

        //    return Json(ilIst);
        //}
        //[HttpPost]
        //public IActionResult AddEmployee([FromBody]PersonEmployee empObj)
        //{
        //    string queryString = "insert into Person.BusinessEntity (rowguid) values(default)" +
        //          "Declare @x int = (select MAX(BusinessEntityID)from Person.BusinessEntity);" +
        //          "insert into Person.Person (BusinessEntityID, PersonType, NameStyle, FirstName, LastName, EmailPromotion, rowguid, ModifiedDate)" +
        //          "values((select MAX(BusinessEntityID)from Person.BusinessEntity),'EM',0,'" + empObj.FirstName.ToString() + "','" + empObj.LastName.ToString() + "',2," +
        //          "(select rowguid from Person.BusinessEntity where BusinessEntityID = @x), SYSDATETIME()); " +
        //          "insert into Person.PersonPhone ( BusinessEntityID, PhoneNumber, PhoneNumberTypeID, ModifiedDate)" +
        //          "values(@x, '" + empObj.PhoneNumber.ToString() + "', '1', SYSDATETIME());" +
        //          "Declare @e int = ((select MAX(EmailAddressID) from Person.EmailAddress)+1);" +
        //          "SET IDENTITY_INSERT  Person.EmailAddress ON;" +
        //          "insert into Person.EmailAddress(BusinessEntityID, EmailAddressID, EmailAddress, rowguid, ModifiedDate)" +
        //          "values(@x, @e, '" + empObj.EmailAddress.ToString() + "', " +
        //          "(select rowguid from Person.Person where BusinessEntityID=@x), SYSDATETIME());" +
        //          "Declare @k int = ((SELECT COUNT(E.BusinessEntityID) FROM HumanResources.Employee E join Person.Person P on E.BusinessEntityID = P.BusinessEntityID WHERE FirstName = '" + empObj.LastName.ToLower().ToString() + "')-1)" +
        //          "insert into HumanResources.Employee( BusinessEntityID, NationalIDNumber, LoginID, JobTitle, BirthDate, MaritalStatus, Gender, " +
        //          "HireDate, SalariedFlag, VacationHours, SickLeaveHours, CurrentFlag, rowguid, ModifiedDate) " +
        //          "values(@x, ('NULL' + (SELECT CAST(@x AS VARCHAR(10)))) , 'globallogic/" + empObj.LastName.ToLower().ToString() + "@k" + "', '" + empObj.JobTitle.ToString() + "', '1997-06-01','S', 'M', SYSDATETIME(), '1', '0', '0', '1'," +
        //          " (select rowguid from Person.BusinessEntity where BusinessEntityID = @x),SYSDATETIME());";

        //    using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=AdventureWorks2014;Integrated Security=True"))
        //    {
        //        SqlCommand command = new SqlCommand(queryString, connection);
        //        connection.Open();
        //        SqlDataReader reader = command.ExecuteReader();
        //        connection.Close();
        //    }          
        //    return Json("OK");
        //}

        //[HttpGet("{Empid}")]
        //public async Task<IActionResult> EmployeeDetails(int Empid)
        //{

        //    var EmpDetails = await (from pers in _context.Person
        //                            join emp in _context.Employee on pers.BusinessEntityId equals emp.BusinessEntityId
        //                            join email in _context.EmailAddress on pers.BusinessEntityId equals email.BusinessEntityId
        //                            join phone in _context.PersonPhone on pers.BusinessEntityId equals phone.BusinessEntityId
        //                            where emp.BusinessEntityId == Empid
        //                            select new
        //                            {
        //                                emp.BusinessEntityId,
        //                                pers.FirstName,
        //                                pers.LastName,
        //                                emp.JobTitle,
        //                                email.EmailAddress1,
        //                                phone.PhoneNumber,
        //                                emp.NationalIdnumber,
        //                                emp.BirthDate
        //                            }
        //                  ).FirstAsync();


        //    return Json(EmpDetails);
        //}

        //[HttpDelete]
        //public IActionResult RemoveEmployeeDetails([FromBody]int empId)
        //{
        //    Employee Emp;
        //    Emp = _context.Employee.Where(x => x.BusinessEntityId == empId).First();
        //    _context.Employee.Remove(Emp);
        //    _context.SaveChanges();



        //    return Json("OK");
        //}
        //[HttpPut]
        //public IActionResult EditEmployee([FromBody]PersonEmployee empData)
        //{
        //    string queryString = "update HumanResources.Employee " +
        //        "set JobTitle = '" + empData.JobTitle + "', " +
        //        "NationalIDNumber = '" + empData.NationalIDNumber + "', " +
        //        "BirthDate = '" + empData.BirthDate + "' " +
        //        "where BusinessEntityID = '" + empData.BusinessEntityID + "'; " +
        //        "update Person.PersonPhone " +
        //        "set PhoneNumber = '" + empData.PhoneNumber + "' " +
        //        "where BusinessEntityID = '" + empData.BusinessEntityID + "'; " +
        //        "update Person.Person " +
        //        "set FirstName = '" + empData.FirstName + "', " +
        //        "LastName = '" + empData.LastName + "' " +
        //        "where BusinessEntityID = '" + empData.BusinessEntityID + "'; " +
        //        "update Person.EmailAddress " +
        //        "set EmailAddress= '" + empData.EmailAddress + "' " +
        //        "where BusinessEntityID = '" + empData.BusinessEntityID + "'; ";        

        //    using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=AdventureWorks2014;Integrated Security=True"))
        //    {
        //        SqlCommand command = new SqlCommand(queryString, connection);
        //        connection.Open();
        //        SqlDataReader reader = command.ExecuteReader();
        //        connection.Close();
        //    }

        //    return Json("OK");
        //}
        #endregion

        [HttpGet]
        public ActionResult Read(int page = 1)
        {
            int pageSize = 20;

            List<Employee> employees_ = null;
            List<Person> people = null;
            List<EmailAddress> email = null;
            List<PersonPhone> phones = null;
            List<PersonEmployee> employees = new List<PersonEmployee>();
          
            using (BussinessTransaction bt = new BussinessTransaction(_unitOfWork))
            {
                bt.Execute(() =>
                {
                    employees_ = _dbEmp.Read().ToList();
                    people = _dbPer.Read().ToList();
                    phones = _dbPhn.Read().ToList();
                    email = _dbEml.Read().ToList();                   
                });
            }        
       
            for (int i = 0; i < employees_.Count(); i++)
            {
                PersonEmployee y = new PersonEmployee();
                y.BusinessEntityID = employees_[i].BusinessEntityId;
                y.BirthDate = employees_[i].BirthDate.ToShortDateString();
                y.NationalIDNumber = employees_[i].NationalIdnumber;
                y.JobTitle = employees_[i].JobTitle;
                Person a = new Person();
                a = people.Find(x => x.BusinessEntityId.Equals(y.BusinessEntityID));
                y.LastName = a.LastName;
                y.FirstName = a.FirstName;
                EmailAddress b = new EmailAddress();
                b = email.Find(x => x.BusinessEntityId.Equals(y.BusinessEntityID));
                y.EmailAddress = b.EmailAddress1;
                PersonPhone c = new PersonPhone();
                c = new PersonPhone();
                c = phones.Find(x => x.BusinessEntityId.Equals(y.BusinessEntityID));
                y.PhoneNumber = c.PhoneNumber;
                employees.Add(y);
            }

            
          
          
               
            //var count = jobTitles.Count();     
            //var items = jobTitles.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            //JobTitlesViewModel employee = new JobTitlesViewModel(count, page, pageSize);
            //IndexJobTitle viewModel = new IndexJobTitle
            //{
            //    JobTitlesViewModel_ = employee,
            //    JobTitles_ = items
            //};
            //return View(viewModel);

            return View(employees);
        }

        [HttpPost]
        public ActionResult Create([FromForm]Address addressData)
        {
            using (BussinessTransaction bt = new BussinessTransaction(_unitOfWork)) // Retry Policy
            {
                bt.Execute(IsolationLevel.ReadUncommitted, () => // Retry Policy
                {
                    if (ModelState.IsValid)   // Without this shit
                    {
                        BusinessEntity o = new BusinessEntity();
                        o.ModifiedDate = DateTime.Now;
                        o.Rowguid = Guid.NewGuid();
                        _dbBis.Create(o);                        
                        _unitOfWork.Save();
                    }
                });
                return RedirectToAction("Read");
            }
        }
    }
    
}