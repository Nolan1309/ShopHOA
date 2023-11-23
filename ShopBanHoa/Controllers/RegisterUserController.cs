using ShopBanHoa.Model_DAO;
using ShopBanHoa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopBanHoa.Controllers
{
    public class RegisterUserController : Controller
    {
        // GET: RegisterUser
        public ActionResult Index()
        {

            return View();
        }

        [HttpGet]
        public ActionResult Register2()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Register2(RegisterAccountModel account)
        {
            if (ModelState.IsValid)
            {
                var accountModel = new AccountModel_User();

                if (accountModel.CheckMail(account.Email))
                {
                    ViewBag.Error1 = "Email đã tồn tại !";

                    //False
                    return View(account);
                }
                else
                {
                    var newAccount = new Account();
                    newAccount.Email = account.Email;
                    newAccount.Phone = account.Phone;
                    newAccount.MatKhau = account.MatKhau;
                    newAccount.FullName = account.FullName;
                  
                    accountModel.SaveAccount(newAccount);
                    return RedirectToAction("Index", "LoginUser");
                }
            }
            else
            {
                
                return View(account);
            }
        }
    }
}