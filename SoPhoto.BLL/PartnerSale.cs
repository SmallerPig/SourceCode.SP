using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoPhoto.BLL
{
    public class PartnerSale : RY.Common.IConsoleListable<Entity.PartnerSale>
    {
        private DAL.PartnerSale helper = new DAL.PartnerSale();

        public void Delete(Entity.PartnerSale t)
        {
            helper.Delete(t);
        }

        public Entity.PartnerSale GetById(int id)
        {
            return helper.GetById(id);
        }

        public int GetCount(bool islocked)
        {
            return helper.GetCount(islocked);
        }

        public IList<Entity.PartnerSale> GetList(int from, int count, bool isLock)
        {
            return helper.GetList(from, count, isLock);
        }

        public Entity.PartnerSale Insert(Entity.PartnerSale t)
        {
            return helper.Insert(t);
        }

        public void Restore(Entity.PartnerSale t)
        {
            helper.Restore(t);
        }

        public void ToRecycle(Entity.PartnerSale t)
        {
            helper.ToRecycle(t);
        }

        public void Update(Entity.PartnerSale t)
        {
            helper.Update(t);
        }

        public IEnumerable<Entity.PartnerSale> GetByPartner(int p)
        {
            return helper.GetByPartner(p);
        }
    }
}
