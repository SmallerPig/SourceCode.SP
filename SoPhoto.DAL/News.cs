using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using SoPhoto.Entity;

namespace SoPhoto.DAL
{
    public class News :RY.Common.IConsoleListable<Entity.SP_News>
    {

        SoPhoto.Entity.SoPhotoDbContext db = new SoPhotoDbContext();

        public void Delete(Entity.SP_News t)
        {
            db.SP_News.Remove(t);
            db.SaveChanges();
        }

        public Entity.SP_News GetById(int id)
        {
            return db.SP_News.SingleOrDefault(item => item.Id == id);
        }

        public int GetCount(bool islocked)
        {
            return db.SP_News.Count(item => item.IsLock == islocked);
        }

        public IList<Entity.SP_News> GetList(int from, int count, bool isLock)
        {
            return db.SP_News.Where(item => item.IsLock == isLock).OrderByDescending(item => item.Id).Skip(from).Take(count).ToList();
        }

        public Entity.SP_News Insert(Entity.SP_News t)
        {
            db.SP_News.Add(t);
            db.SaveChanges();
            return t;
        }

        public void Restore(Entity.SP_News t)
        {
            t.IsLock = false;
            Update(t);
        }

        public void ToRecycle(Entity.SP_News t)
        {
            t.IsLock = true;
            Update(t);
        }

        public void Update(Entity.SP_News t)
        {
            db.Entry(t).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
