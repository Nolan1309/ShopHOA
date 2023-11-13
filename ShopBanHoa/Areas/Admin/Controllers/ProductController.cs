using Dapper;
using PagedList;
using ShopBanHoa.Areas.Admin.DAO_ADMIN;
using ShopBanHoa.Areas.Admin.Models;
using ShopBanHoa.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace ShopBanHoa.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        DataConnection db = new DataConnection();

        // GET: Admin/Product
        public ActionResult Index(string searchstring, int page = 1, int pageSize = 5)
        {

            if (searchstring != null)
            {
                ViewBag.Search = searchstring;
                ProductModel model = new ProductModel();
                var sp = model.GetProductSearch(searchstring,page, pageSize);
                return View(sp);
            }
            else
            {
                
                ProductModel model = new ProductModel();
                var sp = model.GetProduct( page, pageSize);
                return View(sp);

            }
        }
        public void SetSelect()
        {
            CategoryModel catemodel = new CategoryModel();
            var danhMucList = catemodel.Getds();
            ViewBag.DanhMucList = new SelectList(danhMucList, "MaDM", "TenDM");
        }
        [HttpGet]
        public ActionResult Create()
        {
            SetSelect();
            return View();
        }
        [HttpPost]
        public ActionResult Create(SanPham sp, HttpPostedFileBase uploadhinh)
        {
            SetSelect();
            if (ModelState.IsValid)
            {
               
                ProductModel spmodel = new ProductModel();
                spmodel.insert(sp);
                if (uploadhinh != null && uploadhinh.ContentLength > 0)
                {
                    int id;
                    //Lấy ra id của dữ liệu vừa thêm ( lấy hàng cuối cùng ) 
                    using (SqlConnection connection = db.sqlstring())
                    {
                        connection.Open();                     
                        string sql = "SELECT TOP 1 MASP FROM SanPham ORDER BY MASP DESC";
                        using (SqlCommand cmd = new SqlCommand(sql, connection))
                        {                           
                             id = (int)cmd.ExecuteScalar();                           
                        }
                    }
                    //Đổi tên theo từng sản phẩm
                    string _FileName = "";
                    int index = uploadhinh.FileName.IndexOf('.');
                    _FileName = "product" + id.ToString() + "." + uploadhinh.FileName.Substring(index + 1);
                    string _path = Path.Combine(Server.MapPath("~/Assets/admin/images/product"), _FileName);

                    uploadhinh.SaveAs(_path);

                    //Update hình cho sản phẩm
                    using (SqlConnection connection = db.sqlstring())
                    {
                        connection.Open();
                        string sql = "update SanPham set AnhSP = @Hinh where MaSP= @ID";
                        using (SqlCommand cmd = new SqlCommand(sql, connection))
                        {                         
                            cmd.Parameters.Add(new SqlParameter("@Hinh", SqlDbType.NVarChar, 200)).Value = _FileName;
                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = id;                      
                            cmd.ExecuteNonQuery();
                        }
                    }
                  
                }


                return RedirectToAction("Index");

            }
            return View(sp);
        }
        [HttpGet]
        public ActionResult Edit(string id)
        {
           
            ProductModel model = new ProductModel();
            SanPham list = model.GetProductItem(Convert.ToInt32(id));

            CategoryModel catemodel = new CategoryModel();
            var danhMucList = catemodel.Getds();
            ViewBag.DanhMucList = new SelectList(danhMucList, "MaDM", "TenDM");

            return View(list);
        }
        [HttpPost]
        public ActionResult Edit(SanPham sp, HttpPostedFileBase uploadhinh)
        {
            if (ModelState.IsValid)
            {
                ProductModel spmodel = new ProductModel();
                spmodel.update(sp);

                if (uploadhinh != null && uploadhinh.ContentLength > 0)
                {
                   
                    //Đổi tên theo từng sản phẩm
                    string _FileName = "";
                    int index = uploadhinh.FileName.IndexOf('.');
                    _FileName = "product" + sp.MaSP.ToString() + "." + uploadhinh.FileName.Substring(index + 1);
                    string _path = Path.Combine(Server.MapPath("~/Assets/admin/images/product"), _FileName);

                    uploadhinh.SaveAs(_path);

                    //Update hình cho sản phẩm
                    using (SqlConnection connection = db.sqlstring())
                    {
                        connection.Open();
                        string sql = "update SanPham set AnhSP = @Hinh where MaSP= @ID";
                        using (SqlCommand cmd = new SqlCommand(sql, connection))
                        {
                            cmd.Parameters.Add(new SqlParameter("@Hinh", SqlDbType.NVarChar, 200)).Value = _FileName;
                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = sp.MaSP;
                            cmd.ExecuteNonQuery();
                        }
                    }

                }

                return RedirectToAction("Index");
            }
            return View(sp);
        }
        [HttpGet]
        public ActionResult Details(string id)
        {
            ProductModel model = new ProductModel();
            SanPham list = model.GetProductItem(Convert.ToInt32(id));

            return View(list);
        }
        public JsonResult DeleteEmployee(int EmployeeId)
        {
            bool result = false;
            var product = new ProductModel();
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
        //[HttpPost]
        //public ActionResult Delete(Product sp)
        //{

        //        var product = new ProductModel();
        //        int effect = product.DeleteAccount(sp);
        //        if (effect > 0)
        //        {
        //            return RedirectToAction("Index", "Product");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "Xóa sản phẩm thất bại !");

        //        }

        //    return View();
        //}

    }
}