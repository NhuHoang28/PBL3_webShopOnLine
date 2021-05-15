using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class TacGiaDao
    {
        OnlineShopDbContext db = null;
        public TacGiaDao()
        {
            db = new OnlineShopDbContext();
        }
        public List<TacGia> GetAllTacGia()
        {
            return db.TacGias.Where(x => true).ToList();
        }
       

    }

}
