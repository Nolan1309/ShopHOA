using ShopBanHoa.Areas.Admin.Code;
using ShopBanHoa.Model_DAO;
using ShopBanHoa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopBanHoa.Controllers
{
    public class LoginUserController : Controller
    {
        // GET: LoginUser
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginAccountModel account)

        {
            if (ModelState.IsValid)
            {
                var result = new AccountModel_User().Login(account.Email, account.MatKhau);
                if (result)
                {
                    var user = new AccountModel_User().getID(account.Email);
                    var userSession = new UserSession();

                    userSession.UserName = user.Email;
                    userSession.ID = user.AccountID;

                    Session.Add(SessionHelper.USER_SESSION, userSession);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Error = "Tài khoản hoặc mật khẩu không đúng !";
                    return View("Index", account);

                }
            }
            return View("Index");
        }

    }
}