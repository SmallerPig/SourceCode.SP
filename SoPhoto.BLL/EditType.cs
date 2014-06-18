using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoPhoto.BLL
{
    public class EditType
    {
        private DAL.EditType helper;

        public EditType()
        {
            helper = new DAL.EditType();
        }


        public void Insert(Entity.SP_EditType type)
        {
            helper.Insert(type);
        }

        public void DeleteByPicId(int picId)
        {
            helper.DeleteByPicId(picId);
        }

        public IEnumerable<int> GetByPicId(int id)
        {
            return helper.GetByPicId(id);
        }
    }
}
