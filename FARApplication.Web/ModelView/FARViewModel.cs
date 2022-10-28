using FARApplication.Web.ModelView;
using System;
using System.Collections.Generic;


namespace FARApplication.Web.Models
{
    public class FARViewModel
    {
         List<FAR> FARs { get; set; }
        public int PageCount { get; set; }
        public int CurrentPageIndex { get; set; }



    }
}
