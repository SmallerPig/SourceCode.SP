using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoPhoto.Models
{
    public class NewsIndex
    {
        public IEnumerable<Entity.SP_News> Newses { get; set; }

        public IEnumerable<Entity.SP_AD> Ads { get; set; }
    }
}