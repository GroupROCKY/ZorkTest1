using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZorkTest.Entities
{
    abstract class Container
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Dictionary<IN, Thing> contents = new Dictionary<IN, Thing>();

        public Container(string Name, string Description)
        {
            this.Name = Name;
            this.Description = Description;
        }

    }
}
