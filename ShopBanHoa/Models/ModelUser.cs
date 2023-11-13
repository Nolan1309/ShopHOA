using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopBanHoa.Models
{
    public class ModelUser
    {
    }
    public class PhanQuyen
    {
        public int Idphanquyen { get; set; }
        public string tenphanquyen { get; set; }
        public string vaitro { get; set; }
    }

    public class Account
    {
        public int AccountID { get; set; }

        [Required(ErrorMessage = "Email không được để trống!")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ. Email phải có định dạng example@gmail.com")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống!")]
        [MinLength(8, ErrorMessage = "Mật khẩu phải có ít nhất 8 ký tự.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W).+$",
            ErrorMessage = "Mật khẩu phải chứa ít nhất một chữ hoa, một chữ thường, một số và một ký tự đặc biệt.")]
        public string MatKhau { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống!")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Tên không được để trống!")]
        public string FullName { get; set; }


        public DateTime? CreateDate { get; set; }

        [Required(ErrorMessage = "Vai trò không được để trống!")]
        public int IdVaiTro { get; set; }
        public PhanQuyen VaiTro { get; set; } // Mối quan hệ với PhanQuyen
    }

    public class Category
    {
        [Required(ErrorMessage = "Mã không được để trống!")]
        public int MaDM { get; set; }
        [Required(ErrorMessage = "Tên danh mục không được để trống!")]
        public string TenDM { get; set; }
        [Required(ErrorMessage = "Ảnh danh mục không được để trống!")]
        public string AnhDM { get; set; }
        public string MoTaDM { get; set; }
    }

    public class Magiamgia
    {
        public int Idgiamgia { get; set; }
        public string Tenvoucher { get; set; }
        public int Giamgia { get; set; }
    }

    public class SanPham
    {
        [Required(ErrorMessage = "Mã sản phẩm không được để trống!")]
        public int MaSP { get; set; }
        [Required(ErrorMessage = "Mã danh mục không được để trống!")]
        public int MaDM { get; set; }
        [Required(ErrorMessage = "Tên sản phẩm không được để trống!")]
        public string TenSP { get; set; }
        [Required(ErrorMessage = "Ảnh sản phẩm không được để trống!")]
        public string AnhSP { get; set; }
        [Required(ErrorMessage = "Giá sản phẩm không được để trống!")]
        public decimal GiaSP { get; set; }
        [Required(ErrorMessage = "Giá giảm không được để trống!")]
        public decimal GiaSale { get; set; }
        [Required(ErrorMessage = "Số lượng không được để trống!")]
        public int SoLuong { get; set; }
        [Required(ErrorMessage = "Phần trăm giảm giá không được để trống!")]
        public int SalePercent { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime NgaySua { get; set; }
        [Required(ErrorMessage = "Mô tả ngắn không được để trống!")]
        public string MotaShort { get; set; }
        [Required(ErrorMessage = "Mô tả dài không được để trống!")]
        public string MotaDai { get; set; }
        public int SoluongReview { get; set; }
        [Required(ErrorMessage = "Trọng lượng không được để trống!")]

        public int Trongluong { get; set; }
        [Required(ErrorMessage = "Mô tả nguyên liệu không được để trống!")]
        public string nguyenlieu { get; set; }
        public Category DanhMuc { get; set; } // Mối quan hệ với Category
    }

    public class FormReview
    {
        public int IdReview { get; set; }
        public int IdSanpham { get; set; }
        public string tenkhachhang { get; set; }
        public string Thongtin { get; set; }
        public SanPham SanPham { get; set; } // Mối quan hệ với SanPham
        
    }

    public class DonHang
    {
        public int MaDH { get; set; }
        public int MaKH { get; set; }
        public string DiachiKH { get; set; }
        public string EmailKH { get; set; }
        public int SDTKH { get; set; }
        public string noteKH { get; set; }
        public DateTime? NgayOrder { get; set; }
        public DateTime? NgayThanhToan { get; set; }
        public Account KhachHang { get; set; } // Mối quan hệ với Account
    }

    public class ChiTietDonHang
    {
        public int idchitiet { get; set; }
        public int madonhang { get; set; }
        public int MaSP { get; set; }
        public int soluong { get; set; }
        public decimal dongia { get; set; }
        public int? TongTien { get; set; }
        public DonHang DonHang { get; set; } // Mối quan hệ với DonHang
        public SanPham SanPham { get; set; } // Mối quan hệ với SanPham
    }

    public class GopYKien
    {
        public int ID { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public int Sdt { get; set; }
        public string ChuDe { get; set; }
        public string DongGopY { get; set; }
    }

    public class ContentCategory
    {
        public int ID { get; set; }
        public string TenDanhMuc { get; set; }
        public string MotaDanhMuc { get; set; }
    }

    public class Blog
    {
        public int ID { get; set; }
        public int IdContent { get; set; }
        public string TenBlog { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Detail { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescriptions { get; set; }
        public bool? Status { get; set; }
        public int? ViewCount { get; set; }
        public ContentCategory DanhMuc { get; set; } // Mối quan hệ với ContentCategory
    }
}