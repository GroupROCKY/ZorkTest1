using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZorkTest.Entities
{
    class Room : Container
    {
        public Dictionary<Direction, Room> exits = new Dictionary<Direction, Room>();

        public Room(string Name, string Description) : base(Name, Description)
        {
            
        }
    }
}
