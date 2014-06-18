using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SoPhoto.Entity;

namespace SoPhoto.DAL
{
    public class EditType
    {
        Entity.SoPhotoDbContext db = new SoPhotoDbContext();






        public void Insert(SP_EditType type)
        {
            db.EditType.Add(type);
            db.SaveChanges();
        }

        public void DeleteByPicId(int picId)
        {
            db.EditType.Where(item => item.PicId == picId).ToList().ForEach(a => db.EditType.Remove(a));
            db.SaveChanges();
        }

        public IEnumerable<int> GetByPicId(int id)
        {
            return db.EditType.Where(item => item.PicId == id).Select(a => a.Value);
        }
    }
}
