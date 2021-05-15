using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class TheLoaiDao
    {
        OnlineShopDbContext db = null;
        public TheLoaiDao()
        {
            db = new OnlineShopDbContext();
        }
        public List<LoaiSach> GetAllLoaiSach()
        {
            return db.LoaiSaches.Where(x => true).ToList();
        }
        public LoaiSach GetLoaiSachByTenLoaiSach(string tenloaisach)
        {
            return db.LoaiSaches.SingleOrDefault(x => x.TenLoaiSach == tenloaisach);
        }
        public LoaiSach GetLoaiSachByMaLoaiSach(string id)
        {
            return db.LoaiSaches.SingleOrDefault(x => x.MaLoaiSach == id);
        }
        public string insert(LoaiSach l)
        {
            db.LoaiSaches.Add(l);
            db.SaveChanges();
            return l.MaLoaiSach;
        }
    }

}
