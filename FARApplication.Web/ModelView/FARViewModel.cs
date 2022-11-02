using FARApplication.Web.ModelView;
using System;
using System.Collections.Generic;


namespace FARApplication.Web.Models
{
    public class FARViewModel
    {
        public List<FAR> FARs { get; set; }
        public int PageCount { get; set; }
        public int CurrentPageIndex { get; set; }
        public int TotalRecordCount { get; set; }

    }
}
