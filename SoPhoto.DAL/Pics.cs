using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using SoPhoto.Entity;

namespace SoPhoto.DAL
{
    public class Pics : RY.Common.IConsoleListable<Entity.SP_Pics>
    {
        SoPhotoDbContext db = new SoPhotoDbContext();



        public void Delete(Entity.SP_Pics t)
        {
            db.SP_Pics.Remove(t);
            db.SaveChanges();
        }

        public Entity.SP_Pics GetById(int id)
        {
            return db.SP_Pics.SingleOrDefault(item => item.Id == id);
        }


        public IEnumerable<Entity.SP_Pics> Filter(Expression<Func<Entity.SP_Pics, bool>> where,int pageindex,int pageSize)
        {
            return db.SP_Pics.Where(item => item.IsLock == false).Where(where).OrderByDescending(item => item.Id).Skip((pageindex - 1) * pageSize).Take(pageSize);
        }
        
        public int GetCount(bool islocked)
        {
            return db.SP_Pics.Count(item => item.IsLock == islocked);
        }

        public IList<Entity.SP_Pics> GetList(int from, int count, bool isLock)
        {
            return db.SP_Pics.Where(item => item.IsLock == isLock).OrderByDescending(item => item.Id).Skip(from).Take(count).ToList();
        }

        public Entity.SP_Pics Insert(Entity.SP_Pics t)
        {
            db.SP_Pics.Add(t);
            db.SaveChanges();
            return t;
        }

        public void Restore(Entity.SP_Pics t)
        {
            t.IsLock = false;
            Update(t);
        }

        public void ToRecycle(Entity.SP_Pics t)
        {
            t.IsLock = true;
            Update(t);
        }

        public void Update(Entity.SP_Pics t)
        {
            db.Entry(t).State = EntityState.Modified;
            db.SaveChanges();
        }

        public int GetCount()
        {
            return db.SP_Pics.Count();
        }

        public IList<SP_Pics> ConsoleList(int p, int pageSize)
        {
            return db.SP_Pics.OrderByDescending(item => item.Id).Skip(p).Take(pageSize).ToList();
        }

        public IEnumerable<SP_Pics> GetBySelectValue(List<Entity.SelectValue> values,int count)
        {
            var pics = from sv in values
                group sv by sv.PicId
                into v
                where v.Count()>=count
                join p in db.SP_Pics on v.Key equals p.Id
                select p;
            return pics;
        }

        public IList<SP_Pics> SearchByPicCode(string keyword)
        {
            return db.SP_Pics.Where(item => item.PicCode.Contains(keyword)).ToList();
        }

        public SP_Pics GetByCode(string code)
        {
            return db.SP_Pics.FirstOrDefault(item => item.IsLock == false && item.PicCode == code);
        }

        public SP_Pics GetByPicCode(string pic)
        {
            return db.SP_Pics.FirstOrDefault(item => item.PicCode == pic);
        }

        public int CountByFilter(Expression<Func<SP_Pics, bool>> expression)
        {
            return db.SP_Pics.Count(expression);
        }
    }
}
