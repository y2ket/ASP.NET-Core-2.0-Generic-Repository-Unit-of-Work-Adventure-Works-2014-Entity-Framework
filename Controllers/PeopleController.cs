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
        public IActionResult AddEmployee([FromBody]PersonEmployee x ,Employee empObj )
        {
            string connetionString = null;
            SqlConnection connection;
            SqlCommand command;
            string sql = null;

            connetionString = "Data Source=localhost;Initial Catalog=AdventureWorks2014;Integrated Security=True";
            sql = "insert into Person.BusinessEntity (rowguid) values(default)" +
                  "Declare @x int = (select MAX(BusinessEntityID)from Person.BusinessEntity);" +
                  "insert into Person.Person (BusinessEntityID, PersonType, NameStyle, FirstName, LastName, EmailPromotion, rowguid, ModifiedDate)" +
                  "values((select MAX(BusinessEntityID)from Person.BusinessEntity),'EM',0,'" + x.FirstName.ToString() + "','" + x.LastName.ToString() + "',2," +
                  "(select rowguid from Person.BusinessEntity where BusinessEntityID = @x), SYSDATETIME()); " +
                  "insert into Person.PersonPhone ( BusinessEntityID, PhoneNumber, PhoneNumberTypeID, ModifiedDate)" +
                  "values(@x, '" + x.PhoneNumber.ToString() + "', '1', SYSDATETIME());" +
                  "Declare @e int = ((select MAX(EmailAddressID) from Person.EmailAddress)+1);" +
                  "SET IDENTITY_INSERT  Person.EmailAddress ON;" +
                  "insert into Person.EmailAddress(BusinessEntityID, EmailAddressID, EmailAddress, rowguid, ModifiedDate)" +
                  "values(@x, @e, '" + x.EmailAddress.ToString() + "', " +
                  "(select rowguid from Person.Person where BusinessEntityID=@x), SYSDATETIME());";

            string sql3 =
                "Declare @x int = (select MAX(BusinessEntityID)from Person.BusinessEntity);" +
                "DECLARE @k int = ((SELECT COUNT(E.BusinessEntityID) FROM HumanResources.Employee E join Person.Person P on E.BusinessEntityID = P.BusinessEntityID WHERE FirstName = '" + x.LastName.ToLower().ToString() + "')-1)" +
                "insert into HumanResources.Employee( BusinessEntityID, NationalIDNumber, LoginID, JobTitle, BirthDate, MaritalStatus, Gender, " +
                "HireDate, SalariedFlag, VacationHours, SickLeaveHours, CurrentFlag, rowguid, ModifiedDate) " +
                "values(@x, ('NULL' + (SELECT CAST(@x AS VARCHAR(10)))) , 'globallogic/" + x.LastName.ToLower().ToString() +"@k" + "', '"+x.JobTitle.ToString()+"', '1997-06-01','S', 'M', SYSDATETIME(), '1', '0', '0', '1'," +
                " (select rowguid from Person.BusinessEntity where BusinessEntityID = @x),SYSDATETIME()); ";



            connection = new SqlConnection(connetionString);
          
                connection.Open();
                command = new SqlCommand(sql, connection);
                SqlDataReader dr1 = command.ExecuteReader();
                 connection.Close();

            connection.Open();
            command = new SqlCommand(sql3, connection);
            SqlDataReader dr2 = command.ExecuteReader();
            connection.Close();



            //_context.Employee.Add(empObj);
            //_context.SaveChanges();
            return Json("OK");

        }
    }
}