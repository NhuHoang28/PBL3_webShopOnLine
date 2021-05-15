using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ProductDao
    {
        OnlineShopDbContext db = null;
        public ProductDao()
        {
            db = new OnlineShopDbContext();
        }
        public List<SanPham> GetAllSanPham()
        {
            return db.SanPhams.Where(x => true).ToList();
        }
        public List<SanPham> GetBestSellProduct()
        {
            return db.SanPhams.OrderBy(x => x.SoLuong).Take(4).ToList();
        }
        public List<SanPham> GetFeatureProduct()
        {
            return db.SanPhams.OrderBy(x => x.MaLoaiSach).Take(4).ToList();
        }
        public List<SanPham> GetLoaiSachByIDLoaiSach(string id)
        {
            return db.SanPhams.Where(x => x.MaLoaiSach == id).ToList();
        }
        public List<SanPham> GetLoaiSachByIDTacGia(string id)
        {
            return db.SanPhams.Where(x => x.MaTacGia == id).ToList();
        }
        public SanPham GetSanPhamById(string id)
        {
            return db.SanPhams.SingleOrDefault(x => x.MaSach == id);
        }
        public void UpdateSoLuong(int soluong,string id)
        {
            db.SanPhams.SingleOrDefault(x => x.MaSach == id).SoLuong -= soluong;
            db.SaveChanges();
        }
        public List<SanPham> GetSpByTenSach(string tensach)
        {
            var list = new List<SanPham>();
            var authorDao = new AuthorDao();
            foreach(var item in db.SanPhams)
            {
                if (item.TenSach.Contains(tensach)==true||authorDao.GetTacgiaByIDTacgia(item.MaTacGia).TenTacGia.Contains(tensach)==true)
                    list.Add(item);
            }

            return list;
        }
        public string insert(SanPham l)
        {
            db.SanPhams.Add(l);
            db.SaveChanges();
            return l.MaSach;
        }

        public bool Update(SanPham s)
        {
            try
            {
                var n = db.SanPhams.Find(s.MaSach);
                n.MaLoaiSach = s.MaLoaiSach;
                n.MaTacGia = s.MaTacGia;
                n.TenSach = s.TenSach;
                n.NoiDung = s.NoiDung;
                n.HinhAnh = s.HinhAnh;
                n.SoLuong = s.SoLuong;
                n.DonGia = s.DonGia;
                n.TinhTrang = s.TinhTrang;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public void Delete(string masach )
        {
            try
            {
                var n = db.SanPhams.Find(masach);
                db.SanPhams.Remove(n);
                db.SaveChanges();
               
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
