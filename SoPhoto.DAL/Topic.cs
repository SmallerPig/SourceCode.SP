using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using SoPhoto.Entity;

namespace SoPhoto.DAL
{
    public class Topic:RY.Common.IConsoleListable<Entity.SP_Topic>
    {
        SoPhotoDbContext db = new SoPhotoDbContext();

        public Entity.SP_Topic GetById(int id)
        {
            return db.SP_Topic.SingleOrDefault(item => item.Id == id);
        }

        public int GetCount(bool islocked)
        {
            return db.SP_Topic.Count(item => item.IsLock == islocked);
        }

        public IList<Entity.SP_Topic> GetList(int from, int count, bool isLock)
        {
            return db.SP_Topic.Where(item => item.IsLock == isLock).OrderByDescending(item => item.Id).Skip(from).Take(count).ToList();
        }

        public Entity.SP_Topic Insert(Entity.SP_Topic t)
        {
            db.SP_Topic.Add(t);
            db.SaveChanges();
            return t;
        }

        public void ToRecycle(Entity.SP_Topic t)
        {
            t.IsLock = true;
            Update(t);
        }

        public void Delete(Entity.SP_Topic t)
        {
            throw new NotImplementedException();
        }

        public void Restore(Entity.SP_Topic t)
        {
            t.IsLock = false;
            Update(t);
        }

        public void Update(Entity.SP_Topic t)
        {
            db.Entry(t).State = EntityState.Modified;
            db.SaveChanges();
        }

        public IList<SP_Topic> SearchByPicCode(string keyword)
        {
            return db.SP_Topic.Where(item => item.Title.Contains(keyword)).ToList();
        }
    }
}
