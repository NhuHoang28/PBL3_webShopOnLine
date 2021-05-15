using Model.EF;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
   public class ChiTietDonHangDao
    {

        OnlineShopDbContext db = null;
        public ChiTietDonHangDao()
        {
            db = new OnlineShopDbContext();
        }
        public void insert(ChiTietDonHang entity)
        {
            try
            {
                db.ChiTietDonHangs.Add(entity);
                db.SaveChanges();
            }
            catch (Exception e)
            { }

        }
        public List<ChiTietShow> ListChiTiet(long madonhang)
        {
            
            var list = from a in db.SanPhams
                    join b in db.ChiTietDonHangs
on a.MaSach equals b.MaSach
                    where b.MaDonHang == madonhang
                    select new ChiTietShow()
                    {
                        MaSach = a.MaSach,
                        TenSach=a.TenSach,
                        HinhAnh=a.HinhAnh,
                        SoLuong=b.SoLuong,
                        DonGia=b.DonGia,
                        ThanhTien=b.ThanhTien

                    };
            
            return list.ToList();

        }
    }
}
