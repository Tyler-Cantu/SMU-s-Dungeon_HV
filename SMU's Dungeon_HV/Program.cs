using SMU_s_Dungeon_HV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Smu_s_Dungeon
{
    public class Program
    {
        public static Random rand = new Random();
        public static Player currentPlayer = new Player();
        public static bool mainLoop = true;

        static void Main(string[] args)
        {
            Console.Title = "SMU's Dungeon";//title
            Console.BackgroundColor = ConsoleColor.Gray;// background color
            Console.ForegroundColor = ConsoleColor.DarkBlue; //word color
            Console.Clear();//clears page 
            Console.BackgroundColor = ConsoleColor.Gray;// background color
            Console.ForegroundColor = ConsoleColor.DarkBlue;//word color
            Console.WriteLine(@"
 
     ▄████████   ▄▄▄▄███▄▄▄▄   ███    █▄    █   ▄████████                          
    ███    ███ ▄██▀▀▀███▀▀▀██▄ ███    ███   █   ███    ███                          
    ███    █▀  ███   ███   ███ ███    ███       ███    █▀                            
    ███        ███   ███   ███ ███    ███       ███                                  
  ▀███████████ ███   ███   ███ ███    ███     ▀███████████                          
           ███ ███   ███   ███ ███    ███              ███                          
     ▄█    ███ ███   ███   ███ ███    ███        ▄█    ███                          
   ▄████████▀   ▀█   ███   █▀  ████████▀       ▄████████▀                            
                                                                                 
  ████████▄  ███    █▄  ███▄▄▄▄      ▄██████▄     ▄████████  ▄██████▄  ███▄▄▄▄  
  ███   ▀███ ███    ███ ███▀▀▀██▄   ███    ███   ███    ███ ███    ███ ███▀▀▀██▄
  ███    ███ ███    ███ ███   ███   ███    █▀    ███    █▀  ███    ███ ███   ███
  ███    ███ ███    ███ ███   ███  ▄███         ▄███▄▄▄     ███    ███ ███   ███
  ███    ███ ███    ███ ███   ███ ▀▀███ ████▄  ▀▀███▀▀▀     ███    ███ ███   ███
  ███    ███ ███    ███ ███   ███   ███    ███   ███    █▄  ███    ███ ███   ███
  ███   ▄███ ███    ███ ███   ███   ███    ███   ███    ███ ███    ███ ███   ███
  ████████▀  ████████▀   ▀█   █▀    ████████▀    ██████████  ▀██████▀   ▀█   █▀ ");
            Console.ReadLine();// this is the start of the game which wil display the game title and play a tune...still working on being able to skip tune 
            if (!Directory.Exists("saves"))
            {
                Directory.CreateDirectory("saves");
            }
            currentPlayer = Load(out bool newP);
            if (newP)
                Encounters.FirstEncounter();



            while (mainLoop)
            {
                Encounters.RandomEncounter();
            }
        }

        static Player NewStart(int i)
        {
            Console.Clear();
            Player p = new Player();
            int milliseconds = 1000;
            //Music.sweetChild();
            Console.Clear();
            Console.WriteLine("Welcome! Please enter your champion's name!");
            Console.WriteLine("Champion Name:");
            p.name = Console.ReadLine();
            p.id = i;
            //little tune that plays when the character is created
            Console.Beep(400, 200);
            Console.Beep(400, 200);
            Console.Beep(600, 200);
            Console.Beep(700, 200);
            Console.Beep(1000, 500);
            Console.Beep(1000, 500);
            Console.Clear();
            Console.WriteLine("You wake up in a cold, damp stone room under SMU.");
            Thread.Sleep(milliseconds);
            Console.WriteLine("You feel dazed and don't remember anything about your past...");
            Thread.Sleep(milliseconds);



            if (p.name == "")
            {
                Console.WriteLine("You can't even remember your own name...");
                Thread.Sleep(milliseconds);

            }
            else
            {
                Console.WriteLine("The only thing you know is that your name is " + p.name);
                Thread.Sleep(milliseconds);

            }
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("You stumble around the cold, dark room until you feel a door handle.");
            Thread.Sleep(milliseconds);
            Console.WriteLine("There is slight resistance as you turn the handle, but the rusty lock breaks easily...");
            Thread.Sleep(milliseconds);
            Console.WriteLine("You see your captor standing with his back to you outside the door.");
            Console.ReadKey();
            return p;

        }

        public static void Quit()
        {
            Save();
            Environment.Exit(0);
        }
        public static void Save()
        {
            BinaryFormatter binForm = new BinaryFormatter();
            string path = "saves/" + currentPlayer.id.ToString() + ".level";
            FileStream file = File.Open(path, FileMode.OpenOrCreate);
            binForm.Serialize(file, currentPlayer);
            file.Close();
        }
        public static Player Load(out bool newP)
        {
            newP = false;
            Console.Clear();
            string[] paths = Directory.GetFiles("saves");
            List<Player> players = new List<Player>();
            int idCount = 0;

            BinaryFormatter binForm = new BinaryFormatter();
            foreach (string p in paths)
            {
                FileStream file = File.Open(p, FileMode.Open);
                Player player = (Player)binForm.Deserialize(file);
                file.Close();
                players.Add(player);

            }
            idCount = players.Count;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Choose your player:");

                foreach (Player p in players)
                {
                    Console.WriteLine(p.id + ": " + p.name);
                }


                Console.WriteLine("Please input player name or id  (id:# or playername). Additionally, 'create' will start a new save ");
                string[] data = Console.ReadLine().Split(':');

                try //trying code in try block 
                {
                    if (data[0] == "id")
                    {
                        if (int.TryParse(data[1], out int id))
                        {
                            foreach (Player player in players)
                            {
                                if (player.id == id)
                                {
                                    return player;
                                }
                            }
                            Console.WriteLine("There is no player with that id!");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("Your id needs to be a number! Press any key to continue");
                            Console.ReadKey();
                        }
                    }
                    else if (data[0] == "create")
                    {
                        Player newPlayer = NewStart(idCount);
                        newP = true;
                        return newPlayer;

                    }
                    else
                    {
                        foreach (Player player in players)
                        {
                            if (player.name == data[0])
                            {
                                return player;
                            }
                        }
                        Console.WriteLine("There is no player with that name!");
                        Console.ReadKey();
                    }
                }
                catch (IndexOutOfRangeException)//if it finds an exception then it will catch
                                                //to prompt the user that what they did doesnt work
                {
                    Console.WriteLine("Your id needs to be a number! Press any key to continue");
                    Console.ReadKey();
                }

            }
        }
    }
}