using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopBanHoa.Areas.Admin.Code
{
    public static class SessionHelper
    {
        public static string USER_SESSION = "USER_SESSION";
      
        public static  UserSession GetSession()
        {
            var session = HttpContext.Current.Session["loginSession"];
            if(session == null)
            {
                return null;
            }
            else
            {
                return session as UserSession;
            }
        }
    }
}