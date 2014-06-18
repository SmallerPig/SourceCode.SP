using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoPhoto.Entity
{
    public class SelectCondition : RY.Common.Console.IConsoleEntity
    {
        public int Id { get; set; }

        public int ItemId { get; set; }

        public string Name { get; set; }

        public bool IsLock { get; set; }

    }
}
