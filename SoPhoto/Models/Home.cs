using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace SoPhoto.Models
{
    public class Home
    {
        public Home()
        {
        }



        public IEnumerable<Entity.SP_Banner> Banners { get; set; }

        public IEnumerable<Entity.SP_AD> Ads { get; set; }

        public IEnumerable<Entity.SP_Pics> Picses { get; set; }

        public IEnumerable<Entity.SP_Topic> Topics { get; set; }


        public IEnumerable<Entity.SP_News> Newses { get; set; } 
    }
}