﻿
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
        static Dictionary<string, Thing> superList = new Dictionary<string, Thing>();


        static Dictionary<RN, Room> rooms = new Dictionary<RN, Room>();
        static Dictionary<IN, Thing> inventory = new Dictionary<IN, Thing>();

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
            // Rooms set
            Room room1 = new Room("Main Room", "This is the main room.");
            rooms.Add(RN.Main, room1);
            rooms.Add(RN.Living, new Room("Living Room", "This is the Living room."));

            rooms.Add(RN.Second, new Room("Second Room", "This is a Second Room. The door locked behind you."));
            rooms.Add(RN.Cellar, new Room("Cellar aka. Laser Room", "Welcome to the Laser Room. This room is entirely black, windowless, " +
                "countless neon graffiti drawings all over the walls and hundreds of discoballs hanging from the ceiling. " +
                "The room is filled with evil robot penguins who are ready to attack you. So beware! "));

            // Exits setup
            rooms[RN.Main].exits.Add(Direction.East, rooms[RN.Living]);
            rooms[RN.Main].exits.Add(Direction.West, rooms[RN.Second]);
            rooms[RN.Living].exits.Add(Direction.West, rooms[RN.Main]);
            rooms[RN.Cellar].exits.Add(Direction.South, rooms[RN.Living]);

            // Contents setup
            inventory.Add(IN.Poison, new Item("Poison", "This is poison, it will kill you."));
            inventory.Add(IN.LaserGun, new Item("Laser Gun", "A powerful laser gun that will shoot " +
                "your opponent down in an instant."));
            inventory.Add(IN.BodyArmour, new Item("Body Armour", "This is a must have once you've entered " +
                "this crazy room. Don't let the discoballs decieve you."));
            inventory.Add(IN.EvilRobotPeng, new Item("Evil Robot Penguins", "Cute on the outside but devilish on the inside. " +
                "Stay away from these little monsters."));
            inventory.Add(IN.Cage, new Item("Cage", "The only safe spot in this entire room. I hope your flexibale enough to " +
                "crawl into it with all your body armour on."));
            inventory.Add(IN.SmokeBomb, new Item("Smoke Bomb", "Detonates a bomb with lots of thick white smoke."));
            inventory.Add(IN.DiscoBall, new Item("Disco Balls", "Some of them are hanging loose. Maybe they can be used as a weapon?"));




            // Direct Cast
            ((Item)inventory[IN.Poison]).Details = "The label says it will errode metal";
            ((Item)inventory[IN.LaserGun]).Details = "You have limited amunition though so use it wisely.";
            ((Item)inventory[IN.BodyArmour]).Details = "You are not safe here, so gear up solider!";
            ((Item)inventory[IN.EvilRobotPeng]).Details = "They'll bite your finger off if you get too close.";
            ((Item)inventory[IN.SmokeBomb]).Details = "This smoke bomb will give you about 15 seconds of distraction. Use it wisely.";



        }

        static void Main(string[] args)
        {
            SetupGame();
            Room currentRoom = rooms[RN.Main];

            while (true)
            {
                Describe(currentRoom);
                Console.Write(">");
                string command = Console.ReadLine().ToUpper();
                string[] commandList = command.Split(' ');
                if (commandList[0] == "GO" || commandList[0] == "WALK") // Ex. "go east"
                {
                    currentRoom = Go(currentRoom, commandList[1]);
                }
                else if (commandList[0] == "LOOK" || commandList[0] == "CHECK")
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
                    //Console.WriteLine("Try: " + string.Join(", ", Enum.GetNames(typeof(Actions))) + "\n");
                }
            }
        }

        static void LookAt(string command, Room currentRoom)
        {
            if (Enum.TryParse(command, true, out IN itemName))
            {
                if (inventory.TryGetValue(itemName, out Thing item))
                {
                    Console.WriteLine("You're looking at your: " + item.Name);
                    if (item is Item)
                    {
                        if (((Item)item).Details == null)
                            Console.WriteLine("It's not very interesting.");
                        else
                            Console.WriteLine(((Item)item).Details);
                    }
                }
                else if (currentRoom.contents.TryGetValue(itemName, out item))
                {
                    Console.WriteLine("You're looking at the: " + item.Name);
                    if (item is Item)
                    {
                        if (((Item)item).Details == null)
                            Console.WriteLine("It's not very interesting.");
                        else
                            Console.WriteLine(((Item)item).Details);
                    }
                }
            }
            else
            {
                Console.WriteLine("There's nothing like that to look at");
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
                Console.WriteLine("Try: " + string.Join(", ", Enum.GetNames(typeof(Direction))) + "\n");
            }
            return currentRoom;
        }

    }
}
