using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoPhoto.BLL
{
    public class SelectCondition : RY.Common.IConsoleListable<Entity.SelectCondition>
    {
        private DAL.SelectCondition helper = new DAL.SelectCondition();



        public void Delete(Entity.SelectCondition t)
        {
            helper.Delete(t);
        }

        public Entity.SelectCondition GetById(int id)
        {
            return helper.GetById(id);
        }

        public int GetCount(bool islocked)
        {
            return helper.GetCount(islocked);
        }

        public IList<Entity.SelectCondition> GetList(int from, int count, bool isLock)
        {
            return helper.GetList(from, count, isLock);
        }

        public Entity.SelectCondition Insert(Entity.SelectCondition t)
        {
            return helper.Insert(t);
        }

        public void Restore(Entity.SelectCondition t)
        {
            helper.Restore(t);
        }

        public void ToRecycle(Entity.SelectCondition t)
        {
            helper.ToRecycle(t);
        }

        public void Update(Entity.SelectCondition t)
        {
            helper.Update(t);
        }

        public int GetCount(int itemId, bool p)
        {
            return helper.GetCount(itemId, p);
        }

        public IList<Entity.SelectCondition> GetList(int itemId, int p1, int pageSize, bool p2)
        {
            return helper.GetList(itemId,p1,pageSize,p2);
        }

        public IEnumerable<Entity.SelectCondition> ListByItem(int itemId)
        {
            return helper.ListByItemId(itemId);
        }
    }
}
