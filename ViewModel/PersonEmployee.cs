﻿using System;
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
}
