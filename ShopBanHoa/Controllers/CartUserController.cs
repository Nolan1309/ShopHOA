using ShopBanHoa.Connection;
using ShopBanHoa.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
        public ActionResult ThemVaoGio(int MaSP) //Thêm sản phẩm vào giỏ , sản phẩm vẫn đứng yên.
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

            //var sach = db.Saches.FirstOrDefault(s => s.MaSach == masach);
            return RedirectToAction("DetailSanPham", "Product", new { id = MaSP });

        }
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
        public ActionResult Cart(double? giamgia, string coupon_magiam)
        {
            if (Session["Giohang"] == null)
            {
                return RedirectToAction("Index1", "GioHang");
            }
            List<Cart> list = laygiohang();
            ViewBag.TongSOluong = TongSOlUONG();
            decimal tongThanhTien = TongThanhTien();
            ViewBag.TongThanhTien = tongThanhTien;

            // Lấy phần nguyên của TongThanhTien để tính VAT
            int tongThanhTienIntPart = (int)tongThanhTien;

            double VAT = tongThanhTienIntPart * 0.1;
            int VAT2 = (int)VAT;
            ViewBag.VAT = VAT;
            ViewBag.Total = VAT + tongThanhTienIntPart;
            ViewBag.MaCODE = coupon_magiam;
            ViewBag.PhanTramGiam = 0;
            if (giamgia != null)
            {
                ViewBag.PhanTramGiam = giamgia;
                ViewBag.GiamGia = (giamgia / 100) * (VAT + tongThanhTienIntPart);
                ViewBag.Total = (VAT + tongThanhTienIntPart) - (giamgia/100)* (VAT + tongThanhTienIntPart);
            }
            return View(list);
        }
        [HttpPost]
        public ActionResult MaGiamGia(string coupon_magiam)
        {
            if (coupon_magiam != "")
            {
                using (SqlConnection connection = db.sqlstring())
                {
                    connection.Open();
                    string sql = "select Giamgia from Magiamgia where Code = '" + coupon_magiam + "'";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connection))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            // Lấy giá trị giảm giá từ DataTable
                            decimal discountValue = Convert.ToDecimal(dt.Rows[0]["Giamgia"]);

                            // Lưu giá trị giảm giá vào ViewBag
                            ViewBag.giamgia = discountValue;
                        }
                    }
                    connection.Close();
                    return RedirectToAction("Cart", "CartUser", new { giamgia = ViewBag.giamgia, coupon_magiam });
                }
            }
            ViewBag.giamgia = 0;
            return RedirectToAction("Cart", "CartUser");
        }
        public ActionResult XoaGioHang(int masp)
        {
            List<Cart> list = laygiohang();
            Cart sp = list.Single(x => x.MaSP == masp);
            if (sp != null)
            {
                list.RemoveAll(x => x.MaSP == masp);
                return RedirectToAction("Cart", "CartUser");

            }
            if (list.Count == 0)
            {

                return RedirectToAction("Index", "Product");

            }
            return RedirectToAction("Cart", "CartUser");

        }
        public ActionResult CapNhatGioHang(string txtSoLuong, string iMaSach)
        {
            List<Cart> list = laygiohang();
            int masach1 = Convert.ToInt32(iMaSach);
            int newSoLuong = Convert.ToInt32(txtSoLuong);

            Cart giohangToUpdate = list.FirstOrDefault(item => item.MaSP == masach1);
            if (giohangToUpdate != null)
            {

                giohangToUpdate.SoLuong = newSoLuong;
            }

            return RedirectToAction("Cart", "CartUser");
        }
    }
}