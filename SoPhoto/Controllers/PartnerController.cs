using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SoPhoto.Models;

namespace SoPhoto.Controllers
{
    public class PartnerController : Controller
    {
        private BLL.Partner partnerHelper = new BLL.Partner();

        public ActionResult Login()
        {
            Entity.Partner partner = Session["Partner"] as Entity.Partner;
            if (partner!=null)
            {
                return RedirectToAction("Data");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Login(string username,string password)
        {
            Entity.Partner partner = partnerHelper.Login(username, password);
            if (partner != null)
            {
                Session["Partner"] = partner;
                return Json("success");
            }
            return Json("error");
        }



        public ActionResult Data()
        {
            Entity.Partner partner = Session["Partner"] as Entity.Partner;
            if (partner==null)
            {
                return RedirectToAction("Login");
            }
            BLL.PartnerSale saleHelper = new BLL.PartnerSale();
            IEnumerable<Entity.PartnerSale> sales = saleHelper.GetByPartner(partner.Id);
            PartnerData model = new PartnerData()
            {
                Partner = partner,
                Sales = sales
            };


            return View(model);
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(string oldpassword, string newpassword, string validapassword)
        {
            Entity.Partner partner = Session["Partner"] as Entity.Partner;

            if (partner!=null)
            {
                if (newpassword==validapassword && partnerHelper.ValidPartner(partner,oldpassword))
                {
                    partner.PassWord = newpassword;
                    partnerHelper.Update(partner);
                    return Json("success");

                }

            }
            return Json("error");
        }



    }
}
