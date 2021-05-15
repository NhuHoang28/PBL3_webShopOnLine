using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        [HttpGet]
        public ActionResult Index()
        {
            var ListAllProduct = new ProductDao().GetAllSanPham();
            return View(ListAllProduct);
        }
        [HttpGet]
        public ActionResult Category(string id)
        {
            var theloai = new TheLoaiDao().GetLoaiSachByTenLoaiSach(id);
            var listSach = new ProductDao().GetLoaiSachByIDLoaiSach(theloai.MaLoaiSach);
            return View(listSach);
        }
        public ActionResult DetailProduct(string id)
        {
            var p = new ProductDao();
            var t = new AuthorDao();
            var l = new TheLoaiDao();
            var sanpham= p.GetSanPhamById(id);
            ViewBag.RelatedProduct = p.GetLoaiSachByIDTacGia(sanpham.MaTacGia);
            ViewBag.TacGia = t.GetTacgiaByIDTacgia(sanpham.MaTacGia);
            ViewBag.LoaiSach = l.GetLoaiSachByMaLoaiSach(sanpham.MaLoaiSach);
            return View(sanpham);
        }
    }
}