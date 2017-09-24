using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZorkTest.Entities
{
    class Item : Thing
    {
        public string Details { get; set; }

        public Item(string Name, string Description) : base(Name, Description)
        {

        }

        public void LookAt()
        {
            Console.WriteLine(Details);
        }
    }
}
