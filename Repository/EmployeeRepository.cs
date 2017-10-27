using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using EMS2.ViewModel;
using EMS2.Models;

namespace EMS2.Repository
{
    public class EmployeeRepository: IEmployeeRepository
    {
        private AdventureWorks2014Context _context;  

        public void DeleteEmployee(int businessEntityID)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PersonEmployee> GetEmployee()
        {
            throw new NotImplementedException();
        }

        public PersonEmployee GetEmployeeByID(int businessEntityID)
        {
            throw new NotImplementedException();
        }

        public void InsertEmployee(PersonEmployee employee)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(PersonEmployee employee)
        {
            throw new NotImplementedException();
        }

        public void UpdateEmployee(int businessEntityID)
        {
            throw new NotImplementedException();
        }
    }
}
