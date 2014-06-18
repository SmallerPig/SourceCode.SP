using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoPhoto.Entity
{
    public class PersonSexy : RY.Common.Console.IConsoleEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsLock { get; set; }

    }
}
