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
                                  join ea in _context.EmailAddress on per.BusinessEntityId equals ea.BusinessEntityId
                                  select new
                                  {
                                      per.BusinessEntityId,
                                      per.LastName,
                                      per.FirstName,
                                      emp.JobTitle,
                                      ea.EmailAddress1,
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
                Obj.EmailAddress = x.EmailAddress1;
                Obj.PhoneNumber = x.PhoneNumber;
                ilIst.Add(Obj);
            });

            return Json(ilIst);
        }
        [HttpPost]
        public IActionResult AddEmployee([FromBody]PersonEmployee empObj)
        {
            string queryString = "insert into Person.BusinessEntity (rowguid) values(default)" +
                  "Declare @x int = (select MAX(BusinessEntityID)from Person.BusinessEntity);" +
                  "insert into Person.Person (BusinessEntityID, PersonType, NameStyle, FirstName, LastName, EmailPromotion, rowguid, ModifiedDate)" +
                  "values((select MAX(BusinessEntityID)from Person.BusinessEntity),'EM',0,'" + empObj.FirstName.ToString() + "','" + empObj.LastName.ToString() + "',2," +
                  "(select rowguid from Person.BusinessEntity where BusinessEntityID = @x), SYSDATETIME()); " +
                  "insert into Person.PersonPhone ( BusinessEntityID, PhoneNumber, PhoneNumberTypeID, ModifiedDate)" +
                  "values(@x, '" + empObj.PhoneNumber.ToString() + "', '1', SYSDATETIME());" +
                  "Declare @e int = ((select MAX(EmailAddressID) from Person.EmailAddress)+1);" +
                  "SET IDENTITY_INSERT  Person.EmailAddress ON;" +
                  "insert into Person.EmailAddress(BusinessEntityID, EmailAddressID, EmailAddress, rowguid, ModifiedDate)" +
                  "values(@x, @e, '" + empObj.EmailAddress.ToString() + "', " +
                  "(select rowguid from Person.Person where BusinessEntityID=@x), SYSDATETIME());" +
                  "Declare @k int = ((SELECT COUNT(E.BusinessEntityID) FROM HumanResources.Employee E join Person.Person P on E.BusinessEntityID = P.BusinessEntityID WHERE FirstName = '" + empObj.LastName.ToLower().ToString() + "')-1)" +
                  "insert into HumanResources.Employee( BusinessEntityID, NationalIDNumber, LoginID, JobTitle, BirthDate, MaritalStatus, Gender, " +
                  "HireDate, SalariedFlag, VacationHours, SickLeaveHours, CurrentFlag, rowguid, ModifiedDate) " +
                  "values(@x, ('NULL' + (SELECT CAST(@x AS VARCHAR(10)))) , 'globallogic/" + empObj.LastName.ToLower().ToString() + "@k" + "', '" + empObj.JobTitle.ToString() + "', '1997-06-01','S', 'M', SYSDATETIME(), '1', '0', '0', '1'," +
                  " (select rowguid from Person.BusinessEntity where BusinessEntityID = @x),SYSDATETIME());";

            using (SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=AdventureWorks2014;Integrated Security=True"))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                connection.Close();
            }          
            return Json("OK");
        }

        [HttpGet("{Empid}")]
        public async Task<IActionResult> EmployeeDetails(int Empid)
        {

            var EmpDetails = await (from pers in _context.Person
                                    join emp in _context.Employee on pers.BusinessEntityId equals emp.BusinessEntityId
                                    join email in _context.EmailAddress on pers.BusinessEntityId equals email.BusinessEntityId
                                    join phone in _context.PersonPhone on pers.BusinessEntityId equals phone.BusinessEntityId
                                    where emp.BusinessEntityId == Empid
                                    select new
                                    {
                                        emp.BusinessEntityId,
                                        pers.FirstName,
                                        pers.LastName,
                                        emp.JobTitle,
                                        email.EmailAddress1,
                                        phone.PhoneNumber,
                                        emp.NationalIdnumber,
                                        emp.BirthDate
                                    }
                          ).FirstAsync();


            return Json(EmpDetails);
        }
    }
}