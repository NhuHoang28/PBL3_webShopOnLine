using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.Areas.Admin.Data
{
    public class LoginModel
    {
        // hiển thị lỗi nếu chưa nhận đc thông tin
        [Required(ErrorMessage ="Mời nhập User Name")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Mời nhập Password")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}