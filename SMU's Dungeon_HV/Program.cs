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
    [Serializable]
     public class Program
    {
        public static Player currentPlayer = new Player();
        public static bool mainLoop = true;
        
        static void Main(string[] args)
        {
            if (!Directory.Exists("saves")) ;
            {
                Directory.CreateDirectory("saves");
            }

            Console.Title = "SMU's Dungeon";//title
            Console.BackgroundColor = ConsoleColor.Gray;// background color
            Console.ForegroundColor = ConsoleColor.DarkBlue; //word color
            Console.Clear();//clears page 
            Console.BackgroundColor = ConsoleColor.Gray;// background color
            Console.ForegroundColor = ConsoleColor.DarkBlue;//word color
            Start();// this is the start of the game which wil display the game title and play a tune...still working on being able to skip tune 
            Encounters.FirstEncounter();


            while (mainLoop)
            {
                Encounters.RandomEncounter();
            }
        }

        static void Start()
        {
            int milliseconds = 1500;
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
            //Music.sweetChild();
            Console.ReadKey(true);
            Console.Clear();
            Console.WriteLine("Welcome! Please enter your champion's name!");
            Console.WriteLine("Champion Name:");
            currentPlayer.name = Console.ReadLine();
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



            if (currentPlayer.name == "")
            {
                Console.WriteLine("You can't even remember your own name...");
                Thread.Sleep(milliseconds);

            }
            else
            {
                Console.WriteLine("The only thing you know is that your name is " + currentPlayer.name);
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
        }
        public static void Save()
        {
            BinaryFormatter binForm = new BinaryFormatter();
            string path = "saves/" + currentPlayer.id.ToString();
            FileStream file = File.Open(path, FileMode.OpenOrCreate);
            binForm.Serialize(file, currentPlayer);
            file.Close();
        }
        public static Player Load()
        {
            Console.Clear();
            Console.WriteLine("Choose your player:");
            string[] paths = Directory.GetDirectories("saves");
            List<Player> players = new List<Player>();

            BinaryFormatter binForm = new BinaryFormatter();
            foreach (string p in paths)
            {
                FileStream file = File.Open(p, FileMode.Open);
                Player player = (Player)binForm.Deserialize(file);
                file.Close();
            }


        }
    }
}
