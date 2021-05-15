using Model.Dao;
using Model.EF;
using OnlineShop.Common;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace OnlineShop.Controllers
{
    
    public class CartController : Controller
    {
        private string CartSession = "CartSession";
        // GET: Cart
        public  string OverSoLuong = "OverSoLuong";

        public ActionResult Index()
        {
            var cart = Session[CartSession];
            var overList= (List<string>)Session[OverSoLuong];
            if(overList==null)
            {
                string s = "";
                overList = new List<string>();
                overList.Add(s);
            }
            ViewBag.OverSoLuong = overList;
            Session[OverSoLuong] = overList;
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
            
        }
        public JsonResult Update(string cartModel)
        {
            // JavaScriptSerializer chuyển cartModel(Dạng mảng trong javascript thành List kiểu CartItem)
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[CartSession];
            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.SanPham.MaSach == item.SanPham.MaSach);
                if (jsonItem != null)
                {
                    item.SoLuong = jsonItem.SoLuong;
                }
            }
            Session[CartSession] = sessionCart;
            Session[OverSoLuong] = null;
            return Json(new
            {
                status = true
            }); ;

        }
        public JsonResult DeleteAll()
        {
            Session[CartSession] = null;
            Session[OverSoLuong] = null;
            return Json(new
            {
                status = true
            });
        }
        public JsonResult Delete(string id)
        {
            var session = (List<CartItem>)Session[CartSession];
            session.RemoveAll(x => x.SanPham.MaSach == id);
            Session[CartSession] = session;
            Session[OverSoLuong] = null;
            return Json(new
            {
                status = true
            });
        }
        public ActionResult AddItem(string MaSach, int SoLuong)
        {
            if (Session[CommonConstants.USER_SESSION] != null)
            {
                var product = new ProductDao().GetSanPhamById(MaSach);
                var cart = Session[CartSession];
                if (cart != null)
                {
                    var list = (List<CartItem>)cart;
                    if (list.Exists(x => x.SanPham.MaSach == MaSach))
                    {
                        foreach (var item in list)
                        {
                            if (item.SanPham.MaSach == MaSach)
                            {
                                item.SoLuong += SoLuong;
                            }
                        }
                    }
                    else
                    {
                        var item = new CartItem();
                        item.SanPham = product;
                        item.SoLuong = SoLuong;
                        list.Add(item);
                    }
                    Session[CartSession] = list;
                }
                else
                {
                    // Tạo mới đối tượng các item
                    var item = new CartItem();
                    item.SanPham = product;
                    item.SoLuong = SoLuong;
                    var list = new List<CartItem>();
                    list.Add(item);
                    //Gán vào session
                    Session[CartSession] = list;
                }
                return RedirectToAction("Index");
            }
            else return RedirectToAction("Index", "Login");
        }
        public ActionResult Payment()
        {
            var productDao = new ProductDao();
            var list = (List<CartItem>)Session[Common.CommonConstants.CartSession];
            bool test = true;
            Session[OverSoLuong] = null;
            //var overList = (List<string>)Session[OverSoLuong];
            List<string> overList = new List<string>();

            foreach (var item in list)
            {
                if (productDao.GetSanPhamById(item.SanPham.MaSach).SoLuong < item.SoLuong)
                {
                    string s = "        Số lượng trong kho của  " + item.SanPham.TenSach + " chỉ còn " + productDao.GetSanPhamById(item.SanPham.MaSach).SoLuong;
                    
                    overList.Add(s);
                    
                    test = false;
                }
            }
            Session[OverSoLuong] = overList;
            // kiểm tra xem cái sản phẩm trong kho còn đủ k
            if (test)
            {
                return RedirectToAction("Create", "DonHang");
            }
            return RedirectToAction("Index", "Cart");
        }
    }
}
