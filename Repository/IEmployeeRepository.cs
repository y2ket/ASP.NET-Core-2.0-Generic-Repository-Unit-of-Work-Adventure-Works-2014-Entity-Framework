using EMS2.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS2.Repository
{
    public interface  IEmployeeRepository: IDisposable
    {
        IEnumerable<PersonEmployee> GetEmployee();
        PersonEmployee GetEmployeeByID(int businessEntityID);
        void InsertEmployee(PersonEmployee employee);
        void DeleteEmployee(int businessEntityID);
        void UpdateEmployee(int businessEntityID);
        void Update(PersonEmployee employee);
        void Save();

    }
}
