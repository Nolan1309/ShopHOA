using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopBanHoa.Models
{
    public class RegisterAccountModel
    {
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
        [Required(ErrorMessage = "Mật khẩu không được để trống!")]

        public string ConfirmPass { get; set; }
    }
}