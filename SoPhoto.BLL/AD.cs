using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoPhoto.BLL
{
    public class AD:RY.Common.IConsoleListable<Entity.SP_AD>
    {
        private DAL.AD helper;

        public AD()
        {
            helper = new DAL.AD();
        }



        public void Delete(Entity.SP_AD t)
        {
            helper.Delete(t);
        }

        public Entity.SP_AD GetById(int id)
        {
            return helper.GetById(id);
        }

        public int GetCount(bool islocked)
        {
            return helper.GetCount(islocked);
        }

        public IList<Entity.SP_AD> GetList(int from, int count, bool isLock)
        {
            return helper.GetList(from, count, isLock);
        }

        public Entity.SP_AD Insert(Entity.SP_AD t)
        {
            return helper.Insert(t);
        }

        public void Restore(Entity.SP_AD t)
        {
            helper.Restore(t);
        }

        public void ToRecycle(Entity.SP_AD t)
        {
            helper.ToRecycle(t);
        }

        public void Update(Entity.SP_AD t)
        {
            helper.Update(t);
        }
    }
}
