using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoPhoto.BLL
{
    public class SelectItem : RY.Common.IConsoleListable<Entity.SelectItem>
    {
        private DAL.SelectItem helper = new DAL.SelectItem();



        public void Delete(Entity.SelectItem t)
        {
            helper.Delete(t);
        }

        public Entity.SelectItem GetById(int id)
        {
            return helper.GetById(id);
        }

        public int GetCount(bool islocked)
        {
            return helper.GetCount(islocked);
        }

        public IList<Entity.SelectItem> GetList(int from, int count, bool isLock)
        {
            return helper.GetList(from, count, isLock);
        }

        public Entity.SelectItem Insert(Entity.SelectItem t)
        {
            return helper.Insert(t);
        }

        public void Restore(Entity.SelectItem t)
        {
            helper.Restore(t);
        }

        public void ToRecycle(Entity.SelectItem t)
        {
            helper.ToRecycle(t);
        }

        public void Update(Entity.SelectItem t)
        {
            helper.Update(t);
        }

        public IEnumerable<Entity.SelectItem> GetList()
        {
            return helper.List();
        }
    }
}
