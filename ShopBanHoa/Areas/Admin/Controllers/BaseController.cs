﻿using ShopBanHoa.Areas.Admin.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShopBanHoa.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        // GET: Admin/Base
       
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var sess = (UserSession)Session[SessionHelper.USER_SESSION];
            if (sess == null)
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Login", action = "Index", Area = "Admin" }));
            }
            base.OnActionExecuting(filterContext);  
        }
    }
}