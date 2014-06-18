using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoPhoto.Models
{
    public class NewsDetail
    {
        public Entity.SP_News News { get; set; }

        public IEnumerable<Entity.SP_AD> Ads { get; set; }

    }
}