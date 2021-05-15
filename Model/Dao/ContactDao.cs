using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ContactDao
    {
        OnlineShopDbContext db = null;
        public ContactDao()
        {
            db = new OnlineShopDbContext();
        }
        public List<GopY_KH> getAll()
        {
            return db.GopY_KHs.ToList();
        }
        public void ThemGopY(GopY_KH g)
        {
            db.GopY_KHs.Add(g); 
            db.SaveChanges();
        }
    }
}
