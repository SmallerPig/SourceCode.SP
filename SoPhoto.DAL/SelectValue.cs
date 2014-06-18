using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SoPhoto.Entity;

namespace SoPhoto.DAL
{
    public class SelectValue
    {
        Entity.SoPhotoDbContext db = new SoPhotoDbContext();

        public Entity.SelectValue Insert(Entity.SelectValue t)
        {
            db.SelectValue.Add(t);
            db.SaveChanges();
            return t;
        }

        public void Delete(Entity.SelectValue t)
        {
            db.SelectValue.Remove(t);
            db.SaveChanges();
        }

        public IList<Entity.SelectValue> GetByPicId(int picId)
        {
            return db.SelectValue.Where(item => item.PicId == picId).ToList();
        }

        public void DeleteByPic(int p)
        {
            db.SelectValue.Where(item => item.PicId == p).ToList().ForEach(a => db.SelectValue.Remove(a));
            db.SaveChanges();
        }

        public IEnumerable<Entity.SelectValue> GetByFilter(System.Linq.Expressions.Expression<Func<Entity.SelectValue, bool>> expression)
        {
            return db.SelectValue.Where(expression);
        }
    }
}
