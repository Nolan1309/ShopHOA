using ShopBanHoa.Areas.Admin.Code;
using ShopBanHoa.Areas.Admin.DAO_ADMIN;
using ShopBanHoa.Areas.Admin.Models;
using ShopBanHoa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopBanHoa.Areas.Admin.Controllers
{
   
    
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginAccountModel account)
        {
            if (ModelState.IsValid)
            {
                var result = new AccountModel().Login(account.Email, account.MatKhau);
                if (result)
                {
                    var user = new AccountModel().getID(account.Email);
                    var userSession = new UserSession();

                    userSession.UserName = user.Email;
                    userSession.ID = user.AccountID;

                    Session.Add(SessionHelper.USER_SESSION, userSession);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng!");
                    return View("Index");
                  
                }
            }
            return View("Index");
        }
    }
}