using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoPhoto.BLL
{
    public class Banner : RY.Common.IConsoleListable<Entity.SP_Banner>
    {
        private DAL.Banner helper;

        public Banner()
        {
            helper = new DAL.Banner();
        }


        public void Delete(Entity.SP_Banner t)
        {
            helper.Delete(t);
        }

        public Entity.SP_Banner GetById(int id)
        {
            return helper.GetById(id);
        }

        public int GetCount(bool islocked)
        {
            return helper.GetCount(islocked);
        }

        public IList<Entity.SP_Banner> GetList(int from, int count, bool isLock)
        {
            return helper.GetList(from, count, isLock);
        }

        public Entity.SP_Banner Insert(Entity.SP_Banner t)
        {
            return helper.Insert(t);
        }

        public void Restore(Entity.SP_Banner t)
        {
            helper.Restore(t);
        }

        public void ToRecycle(Entity.SP_Banner t)
        {
            helper.ToRecycle(t);
        }

        public void Update(Entity.SP_Banner t)
        {
            helper.Update(t);
        }

        public IList<Entity.SP_Banner> SearchByPicCode(string keyword)
        {
            return helper.SearchByKeyWord(keyword);
        }
    }
}
