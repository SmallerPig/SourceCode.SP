using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoPhoto.Areas.Admin.Controllers
{
    public class PartnerSaleController : BaseController<Entity.PartnerSale>
    {
        private BLL.Partner partnerHelper = new BLL.Partner();




        public PartnerSaleController()
        {
            helper = new BLL.PartnerSale();
        }

        [HttpPost]
        public ActionResult Create(Entity.PartnerSale t)
        {
            Entity.Partner partner = partnerHelper.GetByUserName(t.UserName);
            if (partner!=null)
            {
                t.Id = partner.Id;
                t.UserName = partner.UserName;
                helper.Insert(t);
            }
            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult Edit(Entity.PartnerSale t)
        {
            helper.Update(t);
            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult CheckPicCode(string PicCode)
        { 
            BLL.Pics phelper = new BLL.Pics();
            Entity.SP_Pics p = phelper.GetByPicCode(PicCode);
            if(p !=null)
            { return Json("success"); }
            return Json("error");
        }

        [HttpPost]
        public ActionResult CheckAuthor(string username)
        {
            Entity.Partner partner = partnerHelper.GetByUserName(username);
            if (partner != null)
            { return Json("success"); }
            return Json("error");
        }


    }
}
