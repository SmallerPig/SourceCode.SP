using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoPhoto.DAL
{
    public class Admin
    {
        Entity.SoPhotoDbContext db = new Entity.SoPhotoDbContext();


        public Admin()
        {

        }

        public string Login(Entity.Admin admin)
        {
            var result = from a in db.Admins
                         where a.UserName == admin.UserName && a.Password == admin.Password
                         select a;
            return result.Any() ? "success" : "error";
        }

        public string ChangePassword(Entity.Admin admin)
        {
            db.Entry(admin).State = System.Data.EntityState.Modified;
            db.SaveChanges();
            return "success";
        }

        public Entity.Admin GetAdmin()
        {
            return db.Admins.FirstOrDefault();
        }



        public Entity.Admin ChangePassword(string oldpassword, string newpassword)
        {
            var t = db.Admins.SingleOrDefault(items => items.UserName == "admin" && items.Password == oldpassword);
            if (t != null)
            {
                t.Password = newpassword;
                ChangePassword(t);
            }
            return t;
        }

    }
}
