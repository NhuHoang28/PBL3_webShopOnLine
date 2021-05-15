using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Dao;
using Model.EF;
using OnlineShop.Common;
using OnlineShop.Models;

namespace OnlineShop.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var productDao = new ProductDao();
            ViewBag.bestSellProduct = productDao.GetBestSellProduct();
            ViewBag.FeatureProduct = productDao.GetFeatureProduct();
            return View();
        }
        [ChildActionOnly]
        public ActionResult TheLoai()
        {
            var model = new TheLoaiDao().GetAllLoaiSach();
            return PartialView(model);
        }
        [ChildActionOnly]
        public ActionResult TacGia()
        {
            var model = new TacGiaDao().GetAllTacGia();
            return PartialView(model);
        }
        [ChildActionOnly]
        // hiển thị số hàng trong giỏ ở trang chủ
        public PartialViewResult HeaderCart()
        {
            var cart = Session[CommonConstants.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return PartialView(list);
        }
        [HttpGet]
        public ActionResult Search()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Search(string textSearch)
        {
            var dao = new ProductDao();
            var list = new List<SanPham>();
               list=dao.GetSpByTenSach(textSearch);
            return View(list);
        }
        [HttpGet]
        public ActionResult Contact()
        {
            var userSession = Session[Common.CommonConstants.USER_SESSION];
            if (userSession != null)
            {
                return View();
            }
            else return RedirectToAction("Index", "Login");
        }
        [HttpPost]
        public ActionResult Contact(string Contact,string ID)
        {
            var userSession = Session[Common.CommonConstants.USER_SESSION];
                var gopyDao = new ContactDao();
                var cont = new GopY_KH();
                cont.IdGopY = ID;
                cont.IdUser = ((UserLogin)userSession).UserID;
                cont.NoiDung = Contact;
                cont.NgayGopY = DateTime.Now;
                gopyDao.ThemGopY(cont);
                return RedirectToAction("Index", "Home");
            
        }

    }
}