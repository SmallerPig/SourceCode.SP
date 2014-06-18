using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoPhoto.Areas.Admin.Models
{
    public class PicModel
    {
        public Entity.SP_Pics Pic { get; set; }

        public IList<PicSelect> Selects { get; set; } 
    }
}