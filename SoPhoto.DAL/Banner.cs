using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using SoPhoto.Entity;

namespace SoPhoto.DAL
{
    public class Banner : RY.Common.IConsoleListable<Entity.SP_Banner>
    {
        SoPhotoDbContext db = new SoPhotoDbContext();


        public void Delete(Entity.SP_Banner t)
        {
            throw new NotImplementedException();
        }

        public Entity.SP_Banner GetById(int id)
        {
            return db.SP_Banner.SingleOrDefault(item => item.Id == id);
        }

        public int GetCount(bool islocked)
        {
            return db.SP_Banner.Count(item => item.IsLock == islocked);
        }

        public IList<Entity.SP_Banner> GetList(int from, int count, bool isLock)
        {
            return db.SP_Banner.Where(item => item.IsLock == isLock).OrderByDescending(item => item.Id).Skip(from).Take(count).ToList();
        }

        public Entity.SP_Banner Insert(Entity.SP_Banner t)
        {
            db.SP_Banner.Add(t);
            db.SaveChanges();
            return t;
        }

        public void Restore(Entity.SP_Banner t)
        {
            t.IsLock = false;
            Update(t);
        }

        public void ToRecycle(Entity.SP_Banner t)
        {
            t.IsLock = true;
            Update(t);
        }

        public void Update(Entity.SP_Banner t)
        {
            db.Entry(t).State = EntityState.Modified;
            db.SaveChanges();
        }

        public IList<SP_Banner> SearchByKeyWord(string keyword)
        {
            return db.SP_Banner.Where(item => item.Remark.Contains(keyword)).ToList();
        }
    }
}
