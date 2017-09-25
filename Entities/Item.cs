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

        public bool CanBeTaken { get; set; }
        public string TakeMeText { get; set; }

        public Item(string Name, string Description) : base(Name, Description)
        {

        }

        public void MakeTakeable(string TakeMeText)
        {
            CanBeTaken = true;
            this.TakeMeText = TakeMeText;
        }
    }
}
