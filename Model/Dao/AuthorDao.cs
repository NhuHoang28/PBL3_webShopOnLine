using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class AuthorDao
    {
        OnlineShopDbContext db = null;
        public AuthorDao()
        {
            db = new OnlineShopDbContext();
        }
        public TacGia GetTacgiaByTenTacGia(string tenTacGia)
        {
            return db.TacGias.SingleOrDefault(x => x.TenTacGia == tenTacGia);
        }
        public TacGia GetTacgiaByIDTacgia(string id)
        {
            return db.TacGias.SingleOrDefault(x => x.MaTacGia == id);
        }
        public string insert(TacGia t)
        {
            db.TacGias.Add(t);
            db.SaveChanges();
            return t.MaTacGia;
        }
    }
}
