using EMS2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS2.ViewModel
{
    public class JobTitles
    {
        public int JobTitleId { get; set; }
        public string JobTitle { get; set; }

    }
    public class JobTitlesViewModel
    {
        public int PageNumber { get; private set; }
        public int TotalPages { get; private set; }

        public JobTitlesViewModel(int count, int pageNumber, int pageSize)
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

    public class IndexJobTitle
    {
        public IEnumerable<JobTitles> JobTitles_ { get; set; }
        public JobTitlesViewModel JobTitlesViewModel_ { get; set; }
    }
}


