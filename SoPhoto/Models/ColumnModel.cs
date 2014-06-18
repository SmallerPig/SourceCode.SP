using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SoPhoto.BLL;

namespace SoPhoto.Models
{
    public class ColumnModel
    {

        public Entity.SelectItem SelectItems { get; set; }

        public IEnumerable<Entity.SelectCondition> Conditions
        {
            get
            {
                BLL.SelectCondition helper = new SelectCondition();
                return helper.ListByItem(SelectItems.Id);
            }
        } 


    }
}