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

        public string Login(Entity.SP_Admin admin)
        {
            var result = from a in db.SP_Admins
                         where a.UserName == admin.UserName && a.Password == admin.Password
                         select a;
            return result.Any() ? "success" : "error";
        }

        public string ChangePassword(Entity.SP_Admin admin)
        {
            db.Entry(admin).State = System.Data.EntityState.Modified;
            db.SaveChanges();
            return "success";
        }

        public Entity.SP_Admin GetAdmin()
        {
            return db.SP_Admins.FirstOrDefault();
        }



        public Entity.SP_Admin ChangePassword(string oldpassword, string newpassword)
        {
            var t = db.SP_Admins.SingleOrDefault(items => items.UserName == "admin" && items.Password == oldpassword);
            if (t != null)
            {
                t.Password = newpassword;
                ChangePassword(t);
            }
            return t;
        }


        public Entity.SP_Admin getAdmin()
        {
            return db.SP_Admins.FirstOrDefault();
        }
    }
}
