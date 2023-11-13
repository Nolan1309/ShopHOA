using ShopBanHoa.Connection;
using ShopBanHoa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopBanHoa.Controllers
{
    public class CartUserController : Controller
    {
        DataConnection db = new DataConnection();
        // GET: CartUser
        public ActionResult Index()
        {
            return View();
        }
        public List<Cart> laygiohang() //Kiểm tra tạo ra giỏ hàng nếu không có
        {
            List<Cart> listgh = Session["Giohang"] as List<Cart>;
            if (listgh == null)
            {
                listgh = new List<Cart>();
                Session["Giohang"] = listgh;
            }
            return listgh;
        }
        public ActionResult Themgiohang(int MaSP)//Thêm sản phẩm vào giỏ và tiến đến Giỏ hàng luôn  
        {
            List<Cart> listgh = laygiohang();
            Cart sanpham = listgh.Find(sp => sp.MaSP == MaSP);
            if (sanpham == null)
            {
                sanpham = new Cart(MaSP);
                listgh.Add(sanpham);

            }
            else
            {
                sanpham.SoLuong++;

            }
            return RedirectToAction("Cart", "CartUser");

        }
        //public ActionResult ThemVaoGio(int masach) //Thêm sản phẩm vào giỏ , sản phẩm vẫn đứng yên.
        //{
        //    List<Cart> listgh = laygiohang();
        //    Cart sanpham = listgh.Find(sp => sp.MaSP == MaSP);
        //    if (sanpham == null)
        //    {
        //        sanpham = new Cart(masach);
        //        listgh.Add(sanpham);

        //    }
        //    else
        //    {
        //        sanpham.SoLuong++;

        //    }
        //    var sach = db.Saches.FirstOrDefault(s => s.MaSach == masach);
        //    return RedirectToAction("DetailSach", "Sach", new { id = masach });

        //}
        private int TongSOlUONG()
        {
            int tsl = 0;
            List<Cart> listgiohang = Session["Giohang"] as List<Cart>;
            if (listgiohang != null)
            {
                tsl = listgiohang.Sum(sp => sp.SoLuong);
            }
            return tsl;
        }
        private decimal TongThanhTien()
        {
            decimal ttt = 0;
            List<Cart> listgiohang = Session["Giohang"] as List<Cart>;
            if (listgiohang != null)
            {
                ttt += listgiohang.Sum(sp => sp.ThanhTien);
            }
            return ttt;
        }
        public ActionResult Cart()
        {
            if (Session["Giohang"] == null)
            {
                return RedirectToAction("Index1", "GioHang");
            }
            List<Cart> list = laygiohang();
            ViewBag.TongSOluong = TongSOlUONG();
            ViewBag.TongThanhTien = TongThanhTien();
            return View(list);
        }
    }
}