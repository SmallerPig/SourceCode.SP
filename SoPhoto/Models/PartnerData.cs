using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoPhoto.Models
{
    public class PartnerData
    {
        public Entity.Partner Partner { get; set; }

        public IEnumerable<Entity.PartnerSale> Sales { get; set; } 
    }
}