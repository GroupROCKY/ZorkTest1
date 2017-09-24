using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZorkTest.Entities
{
    abstract class Thing
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Thing(string Name, string Description)
        {

        }
    }
}
