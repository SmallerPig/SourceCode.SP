using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using SoPhoto.Entity;

namespace SoPhoto.DAL
{
    public class Partner:RY.Common.IConsoleListable<Entity.Partner>
    {
        Entity.SoPhotoDbContext db = new SoPhotoDbContext();


        public void Delete(Entity.Partner t)
        {
            db.Partner.Remove(t);
            db.SaveChanges();
        }

        public Entity.Partner GetById(int id)
        {
            return db.Partner.SingleOrDefault(item => item.Id == id);
        }

        public int GetCount(bool islocked)
        {
            return db.Partner.Count(item => item.IsLock == islocked);
        }

        public IList<Entity.Partner> GetList(int from, int count, bool isLock)
        {
            return db.Partner.Where(item => item.IsLock == isLock).OrderByDescending(item => item.Id).Skip(from).Take(count).ToList();
        }

        public Entity.Partner Insert(Entity.Partner t)
        {
            db.Partner.Add(t);
            db.SaveChanges();
            return t;
        }

        public void Restore(Entity.Partner t)
        {
            t.IsLock = false;
            Update(t);
        }

        public void ToRecycle(Entity.Partner t)
        {
            t.IsLock = true;
            Update(t);
        }

        public void Update(Entity.Partner t)
        {
            db.Entry(t).State = EntityState.Modified;
            db.SaveChanges();
        }

        public Entity.Partner Login(string username, string password)
        {
            return db.Partner.FirstOrDefault(item => item.UserName == username && item.PassWord == password);
        }

        public Entity.Partner GetByUserName(string username)
        {
            return db.Partner.FirstOrDefault(item => item.UserName == username);
        }
    }
}
