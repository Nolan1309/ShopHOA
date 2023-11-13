using Dapper;
using PagedList;
using ShopBanHoa.Areas.Admin.DAO_ADMIN;
using ShopBanHoa.Areas.Admin.Models;
using ShopBanHoa.Connection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopBanHoa.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        DataConnection db = new DataConnection();

        public ActionResult Index(string searchstring, int page = 1, int pageSize = 3)
        {
            if (searchstring != null)
            {

                UserModel userModel = new UserModel();
                var accounts = userModel.GetAccountSearch(searchstring,page, pageSize);
                ViewBag.Search = searchstring;
                // Thêm tham số cho tìm kiếm

                return View(accounts);
            }

            else
            {

                UserModel userModel = new UserModel();
                var accounts = userModel.GetAccount(page, pageSize);
                return View(accounts);
            }


            //return View();
        }
        public void SetSelect()
        {
            PhanQuyenModel catemodel = new PhanQuyenModel();
            List<PhanQuyen> phanQuyenList = catemodel.Getds();
            ViewBag.phanquyen = new SelectList(phanQuyenList, "Idphanquyen", "tenphanquyen");
        }
        [HttpGet]
        public ActionResult Create()
        {
            SetSelect();
            return View();
        }
        [HttpPost]
        public ActionResult Create(Account user)
        {
            SetSelect();
            if (ModelState.IsValid)
            {
                var userthem = new UserModel();
                int effect = userthem.Insert(user);
                if (effect > 0)
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm user thất bại !");
                    return View();
                }
            }
            else
            {
                //ModelState.AddModelError(string.Empty, "Vui lòng điền đầy đủ thông tin.");
                return View();
            }
          
        }
        [HttpGet]
        public ActionResult Edit(string id)
        {
            UserModel userModel = new UserModel();
            Account user = userModel.GetAccount(Convert.ToInt32(id));
            return View(user);
        }
        [HttpPost]
        public ActionResult Edit(Account user)
        {
            if (ModelState.IsValid)
            {
                var userthem = new UserModel();
                int effect = userthem.UpdateAccount(user);
                if (effect > 0)
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Sửa user thất bại !");

                }
            }
            return View();
        }
        public JsonResult DeleteUser(int EmployeeId)
        {
            bool result = false;
            var product = new UserModel();
            int effect = product.DeleteAccount(EmployeeId);
            if (effect > 0)
            {
                result = true;
            }
            else
            {
                result = false;

            }


            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //[HttpGet]
        //public ActionResult Delete(string id)
        //{
        //    UserModel userModel = new UserModel();
        //    User user = userModel.GetAccount(Convert.ToInt32(id));
        //    return View(user);
        //}
        //[HttpPost]
        //public ActionResult Delete(User user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var userthem = new UserModel();
        //        int effect = userthem.DeleteAccount(user);
        //        if (effect > 0)
        //        {
        //            return RedirectToAction("Index", "User");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "Xóa user thất bại !");

        //        }
        //    }
        //    return View();
        //}
    }
}