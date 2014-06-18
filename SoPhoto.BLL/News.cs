using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoPhoto.BLL
{
    public class News : RY.Common.IConsoleListable<Entity.SP_News>
    {

        private DAL.News helper = new DAL.News();
        public void Delete(Entity.SP_News t)
        {
            helper.Delete(t);
        }

        public Entity.SP_News GetById(int id)
        {
            return helper.GetById(id);
        }

        public int GetCount(bool islocked)
        {
            return helper.GetCount(islocked);
        }

        public IList<Entity.SP_News> GetList(int from, int count, bool isLock)
        {
            return helper.GetList(from, count, isLock);
        }

        public Entity.SP_News Insert(Entity.SP_News t)
        {
            return helper.Insert(t);
        }

        public void Restore(Entity.SP_News t)
        {
            helper.Restore(t);
        }

        public void ToRecycle(Entity.SP_News t)
        {
            helper.ToRecycle(t);
        }

        public void Update(Entity.SP_News t)
        {
            helper.Update(t);
        }
    }
}
