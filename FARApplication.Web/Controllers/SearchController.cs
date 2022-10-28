using FARApplication.Service.CustomAttribute;
using FARApplication.Web.Models;
using FARApplication.Web.Utility;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace FARApplication.Web.Controllers
{
    public class SearchController : Controller
    {
        [HttpGet("attribute")]
        [CustomHeaderAttribute]
        public JsonResult Index()
        {
                string prefix = HttpContext.Request.Query["prefix"].ToString(); ;
           // List<FAR> result = new List<FAR>();
            List<FAR> FARInfo = new List<FAR>();
            FARInfo = FARUtility.GetALLFAR().Result;
            if (FARInfo.Count > 0)
            {
                var result = FARInfo.Where(p => p.Id.ToString().StartsWith(prefix)).Select(p => new { p.Id, p.Summary }).ToList();

                //var strFAR= JsonConvert.SerializeObject(result);

                return Json(result);
            }
            else
                return Json(null);
        }
    }
}
