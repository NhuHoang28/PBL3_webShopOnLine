using Model.Dao;
using Model.EF;
using OnlineShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
namespace OnlineShop.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        // GET: Admin/User
        // tham số search bên hàm search
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new UserDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        // khai báo sự kiện create user (Khi bấm gán vào sẽ chuyển đến view và xử lý)
        // Tải trang giao diện
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Edit(long id)
        {
            var user = new UserDao().ViewDetail(id);
            return View(user);
        }
        public ActionResult Delete(long id)
        {
            var user = new UserDao().ViewDetail(id);
            return View(user);

        }
        public ActionResult DonHang(long id)
        {
            var dao = new DonHangDao();
            var list = dao.DonHangOfUser(id);
            // Hiển thị list đơn hàng 
            return View(list);
        }
        public ActionResult ChiTietAdmin(long id)
        {
            var dao = new ChiTietDonHangDao();
            var list = dao.ListChiTiet(id);
            // Hiển thị list đơn hàng 
            return View(list);
        }
        public ActionResult GopY()
        {
            var gopyDao = new ContactDao();
            var list = new List<GopY_KH>();
            list = gopyDao.getAll();
            return View(list);
        }
        public ActionResult DsSanPham()
        {
            var sanphamDao = new ProductDao();
            var list = new List<SanPham>();
            list = sanphamDao.GetAllSanPham();
            return View(list);
        }
        public ActionResult ThemLoaiSach()
        {
            return View();
        }
        public ActionResult ThemTacGia()
        {
            return View();
        }
        public ActionResult ThemSanPham()
        {

            return View();
        }

        public ActionResult SuaSanPham(string id)
        {
            var sanphamDao = new ProductDao();
            var s=sanphamDao.GetSanPhamById(id);
            return View(s);
        }
        public ActionResult XoaSanPham(string id)
        {
            var sanphamDao = new ProductDao();

            sanphamDao.Delete(id);
            return RedirectToAction("DsSanPham", "User");
        }
        public ActionResult ShowTacGia()
        {
            var dao = new TacGiaDao();
            var list = new List<TacGia>();
            list = dao.GetAllTacGia();
            
            return View(list);
        }
        public ActionResult ShowLoaiSach()
        {
            var dao = new TheLoaiDao();
            var list = new List<LoaiSach>();
            list = dao.GetAllLoaiSach();

            return View(list);
        }
        [HttpPost]

        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var encryptedMD5Pas = Encryptor.MD5Hash(user.PassWord);
                user.PassWord = encryptedMD5Pas;
                user.KichHoat = true;
                user.TrangThai = true;
                long id = dao.insert(user);
                if (id > 0)
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm user không thành công");
                }
            }
            return View("Index");
        }

        [HttpPost]

        public ActionResult Edit(User user)
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
        [HttpPost]
        public ActionResult Delete(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();

                var result = dao.Delete(user);
                if (result)
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Xóa user không thành công");
                }
            }
            return View("Index");
        }


        [HttpPost]
        public ActionResult ThemLoaiSach(string MaLoaiSach, string TenLoaiSach)
        {
            var theloaiDao = new TheLoaiDao();
            var l = new LoaiSach();
            l.MaLoaiSach = MaLoaiSach;
            l.TenLoaiSach = TenLoaiSach;
            var result = theloaiDao.insert(l);
            return RedirectToAction("Index", "Home");


        }
        [HttpPost]
        public ActionResult ThemTacGia(string MaTacGia, string TenTacGia)
        {

            var dao = new AuthorDao();
            var t = new TacGia();
            t.MaTacGia = MaTacGia;
            t.TenTacGia = TenTacGia;
            var result = dao.insert(t);
            return RedirectToAction("Index", "Home");



        }
        [HttpPost]
        public ActionResult ThemSanPham( string MaTacGia, string MaLoaiSach, string TenSach, string NoiDung, string HinhAnh, string DonGia, string SoLuong, string TinhTrang)
        {

            var dao = new ProductDao();
            var s = new SanPham();
           
            s.MaTacGia = MaTacGia;
            s.MaLoaiSach = MaLoaiSach;
            s.TenSach = TenSach;
            s.NoiDung = NoiDung;
            s.HinhAnh = HinhAnh;
            s.DonGia = Convert.ToInt32(DonGia);
            s.SoLuong = Convert.ToInt32(SoLuong);
            s.TinhTrang = Convert.ToInt32(TinhTrang);
           
            var result = dao.insert(s);

            return RedirectToAction("Index", "Home");

        }

        [HttpPost]

        public ActionResult SuaSanPham(SanPham s)
        {
            
                var dao = new ProductDao();

                var result = dao.Update(s);
               
                    return RedirectToAction("DsSanPham", "User");
               
        }
    }
}