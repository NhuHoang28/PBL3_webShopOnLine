using Model.Dao;
using Model.EF;
using OnlineShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            var user = Session[OnlineShop.Common.CommonConstants.USER_SESSION];
            if (user != null)
            {
                var dao = new DonHangDao();
                var list = dao.DonHangOfUser(((UserLogin)user).UserID);
                // Hiển thị list đơn hàng 
                ViewBag.UserId = ((UserLogin)user).UserID;
                return View(list);
            }
            else return RedirectToAction("Login", "Login");
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpGet]
        public ActionResult EditTTCN(long id)
        {
            var user = new UserDao().ViewDetail(id);
            return View(user);
        }
        public ActionResult LogOut()
        {
            Session[OnlineShop.Common.CommonConstants.USER_SESSION] = null;
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var encryptedMD5Pas = Encryptor.MD5Hash(user.PassWord);
                user.PassWord = encryptedMD5Pas;
                user.KichHoat = false;
                user.TrangThai = false;
                long id = dao.insert(user);
                if (id >0)
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm user không thành công");
                }
            }
            return RedirectToAction("Index","Home");
        }
        
        public ActionResult ChiTiet(long id)
        {
            var dao = new ChiTietDonHangDao();
            var list = dao.ListChiTiet(id);
            return View(list);
        }
        public ActionResult EditTTCN(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();

                var result = dao.Update(user);
                if (result)
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Sửa user không thành công");
                }
            }
            return View("Index");
        }
    }
}