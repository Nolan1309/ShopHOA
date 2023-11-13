using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopBanHoa.Areas.Admin.Models
{
    public class User
    {
        [Required]
        public int AccountID { get; set; }

        [Required]
        [StringLength(100)]
        public string TaiKhoan { get; set; }

        [Required]
        [StringLength(30)]
        public string MatKhau { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        [StringLength(24)]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }
    }
}