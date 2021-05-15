using Model.Dao;
using Model.EF;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class DonHangController : Controller
    {
        // GET: DonHang
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(string TenNguoiNhan, string SDTNguoiNhan, string Email, string DiaChiNhan, string GhiChu)
        {
            var dao = new DonHangDao();
            var productDao = new ProductDao();
            var chitietDao = new ChiTietDonHangDao();
            DonHang don = new DonHang();

            var user = (UserLogin)Session[Common.CommonConstants.USER_SESSION];

            don.IdUser = user.UserID;
            don.NgayDat = DateTime.Now;
            don.TrangThai = 0;
            don.TenNguoiNhan = TenNguoiNhan;
            don.SDTNguoiNhan = SDTNguoiNhan;
            don.Email = Email;
            don.DiaChiNhan = DiaChiNhan;
            don.GhiChu = GhiChu;
            don.TongTien = 0;
            var list = (List<CartItem>)Session[Common.CommonConstants.CartSession];


            var maDonHang = dao.insert(don);
            if (maDonHang >= 0)
            {
                long tongtien = 0;
                foreach (var item in list)
                {
                    ChiTietDonHang entity = new ChiTietDonHang();
                    entity.MaDonHang = maDonHang;
                    entity.MaSach = item.SanPham.MaSach;
                    entity.SoLuong = item.SoLuong;
                    entity.DonGia =(int) item.SanPham.DonGia;
                    entity.ThanhTien = entity.SoLuong * entity.DonGia;
                    chitietDao.insert(entity);
                    productDao.UpdateSoLuong(item.SoLuong, entity.MaSach);
                    tongtien += (long)entity.ThanhTien;
                }
                dao.UpdateTongTien(tongtien, maDonHang);
                Session[Common.CommonConstants.CartSession] = null;
                
                string s = "Bạn đã đặt hàng thành công. Vui lòng xem chi tiết trong tài khoản cá nhân ";
                List<string> overList = new List<string>();
                overList.Add(s);
                Session[Common.CommonConstants.OverSoLuong] = overList;

            }
            return RedirectToAction("Index", "Cart");
        }
    }
}