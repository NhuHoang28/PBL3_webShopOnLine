using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class DonHangDao
    {
        OnlineShopDbContext db = null;
        public DonHangDao()
        {
            db = new OnlineShopDbContext();
        }
        public long insert(DonHang entity)
        {
            try
            {
                db.DonHangs.Add(entity);
                db.SaveChanges();
                return entity.MaDonHang;
            }
            catch (Exception e)
            {
                return -1;
            }
        }
        public List<DonHang> DonHangOfUser(long idUser)
        {
            var list=new List<DonHang>();
            foreach(var item in db.DonHangs)
            {
                if (item.IdUser == idUser)
                    list.Add(item);
            }
            return list;
        }
        public void UpdateTongTien(long tongtien,long MaDonHang)
        {
            db.DonHangs.SingleOrDefault(x => x.MaDonHang == MaDonHang).TongTien = tongtien;
            db.SaveChanges();
        }
        
    }
}
