using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SoPhoto.BLL
{
    public class SelectValue
    {
        DAL.SelectValue helper;

        public SelectValue()
        {
            helper = new DAL.SelectValue();
        }

        public Entity.SelectValue Insert(Entity.SelectValue t)
        {
            return helper.Insert(t);
        }

        public void Delete(Entity.SelectValue t)
        {
            helper.Delete(t);
        }

        public IList<Entity.SelectValue> GetByPicId(int picId)
        {
            return helper.GetByPicId(picId);
        }

        public void DeleteByPic(int p)
        {
            helper.DeleteByPic(p);
        }

        public IEnumerable<Entity.SelectValue> GetByConditionArrary(string[] conditionStrings)
        {
            Expression<Func<Entity.SelectValue, bool>> expression = PredicateBuilder.False<Entity.SelectValue>();

            foreach (var conditionString in conditionStrings)
            {
                int conditionId = 0;
                if (int.TryParse(conditionString, out conditionId))
                {
                    expression = expression.Or(v=>v.ConditionId==conditionId);
                }
            }
            return helper.GetByFilter(expression);
        }
    }
}
