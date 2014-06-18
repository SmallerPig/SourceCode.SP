using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoPhoto.BLL
{
    public class Topic : RY.Common.IConsoleListable<Entity.SP_Topic>
    {
        private DAL.Topic helper;

        public Topic()
        {
            helper = new DAL.Topic();
        }


        public void Delete(Entity.SP_Topic t)
        {
            helper.Delete(t);
        }

        public Entity.SP_Topic GetById(int id)
        {
            return helper.GetById(id);
        }

        public int GetCount(bool islocked)
        {
            return helper.GetCount(islocked);
        }

        public IList<Entity.SP_Topic> GetList(int from, int count, bool isLock)
        {
            return helper.GetList(from, count, isLock);
        }

        public Entity.SP_Topic Insert(Entity.SP_Topic t)
        {
            return helper.Insert(t);
        }

        public void Restore(Entity.SP_Topic t)
        {
            helper.Restore(t);
        }

        public void ToRecycle(Entity.SP_Topic t)
        {
            helper.ToRecycle(t);
        }

        public void Update(Entity.SP_Topic t)
        {
            helper.Update(t);
        }

        public IList<Entity.SP_Topic> SearchByPicCode(string keyword)
        {
            return helper.SearchByPicCode(keyword);
        }
    }
}
