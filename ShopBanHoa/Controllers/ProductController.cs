using ShopBanHoa.Connection;
using ShopBanHoa.Model_DAO;
using ShopBanHoa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopBanHoa.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        DataConnection db = new DataConnection();
        public ActionResult Index(int page = 1, int pageSize = 5)
        {           
            Product_USER sp = new Product_USER();
            var list  = sp.GetAccountList(page,pageSize);
            return View(list);           
        }
        public ActionResult DetailSanPham(int id)
        {
            string masp = id.ToString();            
            Product_USER sp = new Product_USER();
            var list = sp.GetProductItem(masp);

            Product_USER sp1 = new Product_USER();
            List<SanPham> getds5 = sp1.GetProduct_5sanpham();
            ViewBag.ListDS5 = getds5;

            Review_User review = new Review_User();
            List<FormReview> formReviews = review.getds(id);
            ViewBag.Danhsachreview = formReviews;


            return View(list);
        }
    }
}