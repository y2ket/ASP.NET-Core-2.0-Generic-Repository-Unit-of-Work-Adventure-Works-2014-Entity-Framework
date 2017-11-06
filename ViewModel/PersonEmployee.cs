using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS2.ViewModel
{
    public class PersonEmployee
    {
        public int BusinessEntityID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JobTitle { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string NationalIDNumber { get; set; }
        public string BirthDate { get; set; }
        public string Gender { get; set; }
        public string LoginId { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string MaritalStatus { get; set; }

    }
    public class PersonEmployeeViewModel
    {
        public int PageNumber { get; private set; }
        public int TotalPages { get; private set; }

        public PersonEmployeeViewModel(int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageNumber > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageNumber < TotalPages);
            }
        }
    }

    public class IndexPersonEmployeeViewModel
    {
        public List<PersonEmployee> PersonEmployees { get; set; }
        public PersonEmployeeViewModel PersonEmployeeViewModel { get; set; }
    }
}
