using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using SoPhoto.Entity;

namespace SoPhoto.DAL
{
    public class AD : RY.Common.IConsoleListable<Entity.SP_AD>
    {
        Entity.SoPhotoDbContext db = new SoPhotoDbContext();






        public void Delete(Entity.SP_AD t)
        {
            throw new NotImplementedException();
        }

        public Entity.SP_AD GetById(int id)
        {
            return db.SP_AD.SingleOrDefault(item => item.Id == id);
        }

        public int GetCount(bool islocked)
        {
            return db.SP_AD.Count(item => item.IsLock == islocked);
        }

        public IList<Entity.SP_AD> GetList(int from, int count, bool isLock)
        {
            return db.SP_AD.Where(item => item.IsLock == isLock).OrderByDescending(item => item.Id).Skip(from).Take(count).ToList();
        }

        public Entity.SP_AD Insert(Entity.SP_AD t)
        {
            db.SP_AD.Add(t);
            db.SaveChanges();
            return t;
        }

        public void Restore(Entity.SP_AD t)
        {
            t.IsLock = false;
            Update(t);
        }

        public void ToRecycle(Entity.SP_AD t)
        {
            t.IsLock = true;
            Update(t);
        }

        public void Update(Entity.SP_AD t)
        {
            db.Entry(t).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
