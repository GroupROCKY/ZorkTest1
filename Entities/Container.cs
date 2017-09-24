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
        public Dictionary<string, Thing> contents = new Dictionary<string, Thing>();

        public Container(string Name, string Description)
        {
            this.Name = Name;
            this.Description = Description;
        }

    }
}
