using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FARApplication.Web
{
    public class AuthorizeActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            
             
            Controller controller = filterContext.Controller as Controller;

            var descriptor = filterContext.ActionDescriptor as ControllerActionDescriptor;
           // var actionName = descriptor.ActionName;
            var controllerName = descriptor.ControllerName;
        



            if (controller != null)
            {
                if (controllerName != "Login")
                {
                    if (filterContext.HttpContext.Session != null && filterContext.HttpContext.Session.GetString("UserEmail") == null)
                    {
                    filterContext.Result =
                           new RedirectToRouteResult(
                               new RouteValueDictionary{{ "controller", "Login" },
                                          { "action", "Login" }

                                                             });
                    }
                }
                else
                {
                    if (filterContext.HttpContext.Session != null && filterContext.HttpContext.Session.GetString("UserEmail") != null)
                    {
                        filterContext.Result =
                               new RedirectToRouteResult(
                                   new RouteValueDictionary{{ "controller", "Home" },
                                          { "action", "Index" }

                                                                 });
                    }
                }
            }

            base.OnActionExecuting(filterContext);

        }
    }

   
}
