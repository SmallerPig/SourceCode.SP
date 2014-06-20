using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoPhoto.BLL
{
    public class AdminManage
    {
        DAL.Admin helper = new DAL.Admin();

        public AdminManage()
        {

        }

        public string Login(Entity.SP_Admin admin)
        {
            admin.Password = RY.Common.ASE.EncryptCode(admin.Password);
            return helper.Login(admin);
        }

        public string ChangePassword(Entity.SP_Admin admin)
        {
            admin.Password = RY.Common.ASE.EncryptCode(admin.Password);
            return helper.ChangePassword(admin);
        }

        public Entity.SP_Admin ChangePassword(string oldPassword, string newPassword)
        {
            oldPassword = RY.Common.ASE.EncryptCode(oldPassword);
            newPassword = RY.Common.ASE.EncryptCode(newPassword);
            return helper.ChangePassword(oldPassword, newPassword);
        }


        public Entity.SP_Admin GetAdmin()
        {
            return helper.GetAdmin();
        }



        public Entity.SP_Admin getAdmin()
        {
            return helper.getAdmin();
        }
    }
}
