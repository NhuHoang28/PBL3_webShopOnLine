using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
using PagedList;
namespace Model.Dao
{
    //để public để các project khác có thể truy cập được
    public class UserDao
    {
        OnlineShopDbContext db = null;
        public UserDao()
        {
            db = new OnlineShopDbContext();
        }
        public long insert(User entity)
        {
            db.Users.Add(entity);
            db.SaveChanges();
            return entity.IdUser;
        }
        public IEnumerable<User> ListAllPaging(string searchString, int page, int pageSize)
        {
            // Lấy ra đúng số bản ghi của trang đó
            IOrderedQueryable<User> model = db.Users.OrderByDescending(x => x.IdUser);
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.UserName.Contains(searchString) || x.HoTen.Contains(searchString)).OrderByDescending(x => x.IdUser);
            }
            return model.ToPagedList(page, pageSize);
        }
        public bool Update(User entity)
        {
            try
            {
                var user = db.Users.Find(entity.IdUser);
                user.HoTen = entity.HoTen;
                user.UserName = entity.UserName;
                user.DiaChi = entity.DiaChi;
                user.Email = entity.Email;
                user.DienThoai = entity.DienThoai;
                user.KichHoat = entity.KichHoat;
                user.GioiTinh = entity.GioiTinh;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool UserUpdate(User entity)
        {
            try
            {
                var user = db.Users.Find(entity.IdUser);
                user.HoTen = entity.HoTen;
                user.UserName = entity.UserName;
                user.PassWord = entity.PassWord;
                user.DiaChi = entity.DiaChi;
                user.Email = entity.Email;
                user.DienThoai = entity.DienThoai;
                user.KichHoat = entity.KichHoat;
                user.GioiTinh = entity.GioiTinh;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Delete(User user)
        {
            try
            {
                User u = ViewDetail(user.IdUser);
                db.Users.Remove(u);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public User GetByUserName(string username)
        {
            return db.Users.SingleOrDefault(x => x.UserName == username);
        }
        public User ViewDetail(long id)
        {
            User y = db.Users.Find(id);
            return y;

        }
        public int UserLogin(string name, string password)
        {
            var result = db.Users.SingleOrDefault(x => x.UserName == name);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (result.PassWord == password)
                {
                    if (result.KichHoat == false)
                        return -1;
                    return 1;
                }
                else return -2;
            }
        }

    }
}
