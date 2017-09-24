
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZorkTest.Entities;

namespace ZorkTest
{
    class Program
    {
        static Dictionary<string, Room> rooms = new Dictionary<string, Room>();
        static Dictionary<string, Thing> inventory = new Dictionary<string, Thing>();

        static void Describe(Room room)
        {
            string exitsText = string.Join(", ", room.exits.Keys.ToArray());
            if (string.IsNullOrEmpty(exitsText))
            {
                exitsText = "None";
            }
            Console.WriteLine("{0}\n\n{1}\n\nExits Are: {2}\n", room.Name, room.Description, exitsText);
        }

        static void SetupGame()
        {
            // Rooms
            rooms.Add("Start", new Room("Start", "This is the main room."));
            rooms.Add("First Room", new Room("First Room", "This is the first room."));
            rooms.Add("Second Room", new Room("Second Room", "This is a Second Room. The door locked behind you."));

            // Exits
            rooms["Start"].exits.Add(Direction.East, rooms["First Room"]);
            rooms["Start"].exits.Add(Direction.West, rooms["Second Room"]);
            rooms["First Room"].exits.Add(Direction.West, rooms["Start"]);

            // Contents
            inventory.Add("POISON", new Item("Poison", "This is poison, it will kill you."));
            inventory["POISON"].Description = "The label says it will errode metal";
            rooms["Start"].contents.Add("Second Room Key", new Key("Second Room Key", "It's a key to the Second Room."));
            
        }

        static void Main(string[] args)
        {
            SetupGame();
            Room currentRoom = rooms["Start"];

            while (true)
            {
                Describe(currentRoom);
                Console.Write(">");
                string command = Console.ReadLine().ToUpper();
                string[] commandList = command.Split(' ');

                if (commandList[0] == "GO" && commandList.Length == 2) // Ex. "go east"
                {
                    currentRoom = Go(currentRoom, commandList[1]);
                }
                if (commandList[0] == "LOOK" && commandList[1] == "AT")
                {
                    LookAt(commandList[2], currentRoom);
                }

                else if (command == "QUIT")
                {
                    return;
                }
                else
                {
                    Console.WriteLine("I don't understand.");
                    Console.WriteLine("Try: " + string.Join(", ", Enum.GetNames(typeof(Actions))) + "\n");
                }
            }
        }

        static void LookAt(string item, Room currentRoom)
        {
            if (currentRoom.Name.ToUpper() == item) // Looking at current room
            {
                Console.WriteLine("You look around: " + currentRoom.Description);
                string exitsText = string.Join(", ", currentRoom.exits.Keys.ToArray());
                if (string.IsNullOrEmpty(exitsText))
                {
                    exitsText = "None";
                }
                Console.WriteLine("The exits are to your " + exitsText);
            }
            else if (currentRoom.contents.ContainsKey(item)) // Looking at an item inside of current room
            {
                Console.WriteLine("You look at the " + currentRoom.contents[item].Name +". " +currentRoom.contents[item].Description);
            }
            else if (inventory.ContainsKey(item))
            {
                Console.WriteLine("You look at your " + inventory[item].Name + ". " + inventory[item].Description);
            }
            else
            {
                Console.WriteLine("Theres nothing like that to look at");
            }
        }

        static Room Go(Room currentRoom, string command)
        {
            if (Enum.TryParse(command, true, out Direction direction)) // Tries to parse input as one of the Enums (case insensitive)
            {
                if (currentRoom.exits.TryGetValue(direction, out Room destination))
                {
                    currentRoom = destination;
                }
                else
                {
                    Console.WriteLine("You can't go that way.");
                }
            }
            else
            {
                Console.WriteLine("I don't understand.");
            }
            return currentRoom;
        }

    }
}
