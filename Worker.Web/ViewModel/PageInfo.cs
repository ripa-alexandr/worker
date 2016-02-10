using System;

namespace Worker.Web.ViewModel
{
    public class PageInfo
    {
        public int? CurrentPage { get; set; }

        public int? PageSize { get; set; }

        public int? TotalItems { get; set; }
    }
}