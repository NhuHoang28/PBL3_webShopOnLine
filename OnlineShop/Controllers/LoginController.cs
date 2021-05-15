using Model.Dao;
using OnlineShop.Areas.Admin.Data;
using OnlineShop.Common;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }
        //trong code view sẽ gán hàm này cho nút login
        public ActionResult Login(LoginModel model)
        {
            // kiểm tra vượt qua cái form rỗng r
            if(ModelState.IsValid)
            {
                var dao = new UserDao();
                // password đã được mã hóa
                var result = dao.UserLogin(model.UserName, Encryptor.MD5Hash(model.Password));
                if (result==1)
                {
                    var admin = dao.GetByUserName(model.UserName);
                    var AdminSession = new UserLogin();
                    AdminSession.UserName = admin.UserName;
                    AdminSession.UserID = admin.IdUser;
                    Session.Add(CommonConstants.USER_SESSION, AdminSession);
                    if (admin.TrangThai)
                        return RedirectToAction("Index", "Admin/Home");
                    else return RedirectToAction("Index","Home");
                }
                else if(result==-2)
                {
                    ModelState.AddModelError("", "Password không đúng");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản chưa được kích hoạt");
                }
            }
            // Nếu k trả về trang index
            
                return View("Index");
            
            
        }
    }
}