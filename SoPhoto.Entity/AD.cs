using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoPhoto.Entity
{
    public class SP_AD:RY.Common.Console.IConsoleEntity
    {
        public int Id { get; set; }

        public string LinkAddress { get; set; }

        public string Address { get; set; }

        public string Remark { get; set; }

        public bool IsLock { get; set; }


    }
}
