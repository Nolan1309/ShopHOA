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
using System.Reflection;
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
        public ActionResult Create(SanPham sp)
        {
            SetSelect();
            if (ModelState.IsValid)
            {
                var sqlParameter = new SqlParameter
                {
					ParameterName = "MaSP",
					SqlDbType = SqlDbType.Int,
					Direction = ParameterDirection.Output
				};
				List<string> imageUrls = sp.Images.Split(',').ToList();
				ProductModel spmodel = new ProductModel();
                spmodel.insert(sp, sqlParameter);
				int maSPValue = (int)sqlParameter.Value;
				foreach (var item in imageUrls)
                {
                    ImageProductModel image = new ImageProductModel();
					image.insertImageProduct(maSPValue, item);
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
			ImageProductModel imageProductModel = new ImageProductModel();
			List<string> ListImgs = imageProductModel.getImageProduct(Convert.ToInt32(id));
            list.Images = string.Join(",", ListImgs);
			CategoryModel catemodel = new CategoryModel();
            var danhMucList = catemodel.Getds();
            ViewBag.DanhMucList = new SelectList(danhMucList, "MaDM", "TenDM");

            return View(list);
        }
        [HttpPost]
        public ActionResult Edit(SanPham sp)
        {
            if (ModelState.IsValid)
            {
                ProductModel spmodel = new ProductModel();
                ImageProductModel imageProductModel = new ImageProductModel();
                spmodel.update(sp);
                imageProductModel.deleteImageProduct(sp.MaSP);
				List<string> imageUrls = sp.Images.Split(',').ToList();
                foreach (var item in imageUrls)
                {
					ImageProductModel image = new ImageProductModel();
					image.insertImageProduct(sp.MaSP, item);
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
			ImageProductModel imageProductModel = new ImageProductModel();
			imageProductModel.deleteImageProduct(EmployeeId);
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