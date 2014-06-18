using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using SoPhoto.Entity;

namespace SoPhoto.DAL
{
    public class PartnerSale:RY.Common.IConsoleListable<Entity.PartnerSale>
    {
        SoPhoto.Entity.SoPhotoDbContext db = new SoPhotoDbContext();
        public void Delete(Entity.PartnerSale t)
        {
            db.PartnerSale.Remove(t);
            db.SaveChanges();
        }

        public Entity.PartnerSale GetById(int id)
        {
            return db.PartnerSale.SingleOrDefault(item => item.Id == id);
        }

        public int GetCount(bool islocked)
        {
            return db.PartnerSale.Count(item => item.IsLock == islocked);
        }

        public IList<Entity.PartnerSale> GetList(int from, int count, bool isLock)
        {
            return db.PartnerSale.Where(item => item.IsLock == isLock).OrderByDescending(item => item.Id).Skip(from).Take(count).ToList();
        }

        public Entity.PartnerSale Insert(Entity.PartnerSale t)
        {
            db.PartnerSale.Add(t);
            db.SaveChanges();
            return t;
        }

        public void Restore(Entity.PartnerSale t)
        {
            t.IsLock = false;
            Update(t);
        }

        public void ToRecycle(Entity.PartnerSale t)
        {
            t.IsLock = true;
            Update(t);
        }

        public void Update(Entity.PartnerSale t)
        {
            db.Entry(t).State = EntityState.Modified;
            db.SaveChanges();
        }

        public IEnumerable<Entity.PartnerSale> GetByPartner(int p)
        {
            return db.PartnerSale.Where(item => item.PartnerId == p&&item.IsLock==false);
        }
    }
}
