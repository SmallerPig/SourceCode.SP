using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoPhoto.BLL
{
    public class Partner : RY.Common.IConsoleListable<Entity.Partner>
    {
        private DAL.Partner helper = new DAL.Partner();


        public void Delete(Entity.Partner t)
        {
            helper.Delete(t);
        }

        public Entity.Partner GetById(int id)
        {
            return helper.GetById(id);
        }

        public int GetCount(bool islocked)
        {
            return helper.GetCount(islocked);
        }

        public IList<Entity.Partner> GetList(int from, int count, bool isLock)
        {
            return helper.GetList(from, count, isLock);
        }

        public Entity.Partner Insert(Entity.Partner t)
        {
            t.PassWord = RY.Common.ASE.EncryptCode(t.PassWord);
            return helper.Insert(t);
        }

        public void Restore(Entity.Partner t)
        {
            helper.Restore(t);
        }

        public void ToRecycle(Entity.Partner t)
        {
            helper.ToRecycle(t);
        }

        public void Update(Entity.Partner t)
        {
            t.PassWord = RY.Common.ASE.EncryptCode(t.PassWord);
            helper.Update(t);
        }

        public Entity.Partner Login(string username, string password)
        {
            password = RY.Common.ASE.EncryptCode(password);
            return helper.Login(username, password);
        }

        public bool ValidPartner(Entity.Partner partner, string oldpassword)
        {
            oldpassword = RY.Common.ASE.EncryptCode(oldpassword);
            return partner.PassWord == oldpassword;
        }

        public Entity.Partner GetByUserName(string username)
        {
            return helper.GetByUserName(username);
        }
    }
}
