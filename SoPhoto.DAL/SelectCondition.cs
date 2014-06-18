using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using SoPhoto.Entity;

namespace SoPhoto.DAL
{
    public class SelectCondition : RY.Common.IConsoleListable<Entity.SelectCondition>
    {
        Entity.SoPhotoDbContext db = new SoPhotoDbContext();




        public void Delete(Entity.SelectCondition t)
        {
            throw new NotImplementedException();
        }

        public Entity.SelectCondition GetById(int id)
        {
            return db.SelectCondition.SingleOrDefault(item => item.Id == id);
        }

        public int GetCount(bool islocked)
        {
            return db.SelectCondition.Count(item => item.IsLock == islocked);
        }

        public IList<Entity.SelectCondition> GetList(int from, int count, bool isLock)
        {
            return db.SelectCondition.Where(item => item.IsLock == isLock).OrderByDescending(item => item.Id).Skip(from).Take(count).ToList();
        }

        public Entity.SelectCondition Insert(Entity.SelectCondition t)
        {
            db.SelectCondition.Add(t);
            db.SaveChanges();
            return t;
        }

        public void Restore(Entity.SelectCondition t)
        {
            t.IsLock = false;
            Update(t);
        }

        public void ToRecycle(Entity.SelectCondition t)
        {
            t.IsLock = true;
            Update(t);
        }

        public void Update(Entity.SelectCondition t)
        {
            db.Entry(t).State = EntityState.Modified;
            db.SaveChanges();
        }

        public int GetCount(int itemId, bool p)
        {
            return db.SelectCondition.Count(item => item.IsLock == p&&item.ItemId==itemId);
        }

        public IList<Entity.SelectCondition> GetList(int itemId, int p1, int pageSize, bool p2)
        {
            return db.SelectCondition.Where(item => item.IsLock == false && item.ItemId == itemId).OrderByDescending(item => item.Id).Skip(p1).Take(pageSize).ToList();
        }

        public IEnumerable<Entity.SelectCondition> ListByItemId(int itemId)
        {
            return db.SelectCondition.Where(item => item.IsLock == false && item.ItemId == itemId);
        }
    }
}
