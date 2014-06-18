using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using SoPhoto.Entity;

namespace SoPhoto.DAL
{
    public class SelectItem :RY.Common.IConsoleListable<Entity.SelectItem>
    {

        Entity.SoPhotoDbContext db = new SoPhotoDbContext();

        public void Delete(Entity.SelectItem t)
        {
            throw new NotImplementedException();
        }

        public Entity.SelectItem GetById(int id)
        {
            return db.SelectItem.SingleOrDefault(item => item.Id == id);
        }

        public int GetCount(bool islocked)
        {
            return db.SelectItem.Count(item => item.IsLock == islocked);
        }

        public IList<Entity.SelectItem> GetList(int from, int count, bool isLock)
        {
            return db.SelectItem.Where(item => item.IsLock == isLock).OrderByDescending(item => item.Id).Skip(from).Take(count).ToList();
        }

        public Entity.SelectItem Insert(Entity.SelectItem t)
        {
            db.SelectItem.Add(t);
            db.SaveChanges();
            return t;
        }

        public void Restore(Entity.SelectItem t)
        {
            t.IsLock = false;
            Update(t);
        }

        public void ToRecycle(Entity.SelectItem t)
        {
            t.IsLock = true;
            Update(t);
        }

        public void Update(Entity.SelectItem t)
        {
            db.Entry(t).State = EntityState.Modified;
            db.SaveChanges();
        }

        public IEnumerable<Entity.SelectItem> List()
        {
            return db.SelectItem.Where(item => item.IsLock == false);
        }
    }
}
