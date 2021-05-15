using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class AuthorController:Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Author(string id)
        {
            var TacGia = new AuthorDao().GetTacgiaByTenTacGia(id);
            var listAuthor = new ProductDao().GetLoaiSachByIDTacGia(TacGia.MaTacGia);
            return View(listAuthor);
        }
    }
}