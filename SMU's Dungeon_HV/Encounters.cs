using Smu_s_Dungeon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMU_s_Dungeon_HV 
{ 
public class Encounters
    {


    static Random rand = new Random();
    //Encounter Generic

        



    //Encounters
    public static void FirstEncounter()
    {
        int milliseconds = 2000;
        int millsec2 = 1500;
        Console.Clear();// clears the page and this begins first encounter of the game
        Console.WriteLine("You throw open the door and grab a wooden cross while charging towards your captor!");
        Thread.Sleep(milliseconds);
        Console.WriteLine("He turns...");
        Thread.Sleep(millsec2);
        Console.WriteLine("What do you do?");
        Console.WriteLine();
        Console.ReadKey();
        Combat(false, "Human Rogue", 1, 5);// generates a "Human Rogue" with 1 power(attack damage) and 5 health.
        }
    public static void BasicFightEncounter() // this is the start of every encounter after. 
    {
        Console.Clear();
        Console.WriteLine("You run down the hall and turn the corner... you see a shadowy figure...");
        Console.ReadKey();
        Combat(true, "", 0, 0);// generates one of the random encounters which at the current time is only a "Zombie","Human Cultist","Skeleton" or "Grave Robber". 
            //Also in this Basic Fight Encounter I added a new Encounter which can randomly generate a "Dark Wizard" with 4 power(attack damage) and 2 health.
    }
    public static void WizardEncounter()
        {//new random encounter
            int milliseconds = 2000;

            Console.Clear();
            Console.WriteLine("There is a door to your right...it slowly creaks open... ");
            Thread.Sleep(milliseconds);

            Console.Write("As you peer into the dark room. You see a gnome, with a scroll that is much bigger than him, practicing dark magic.");
            Console.ReadKey();
            Combat(false, "Dark Wizard", 4, 2);
        }



    // Encounter Tools
    public static void RandomEncounter()
    {//This is the Random Encounter generator... you can see here it will either give you the basic fight encounter or the Wizard encounter. More will be added later
            //such as a boss fights.
        switch (rand.Next(0, 2))
        {
            case 0:
                BasicFightEncounter();
                break;
            case 1:
                    WizardEncounter();
                break ;
        }
    }
    public static void Combat(bool random, string name, int power, int health)
    {
        string n = "";
        int p = 0;
        int h = 0;

        if (random)
        {
            n = GetName();
            p = Program.currentPlayer.GetPower();  //was rand.Next(1, 5);
            h = Program.currentPlayer.GetHealth();//was rand.Next(1, 8);
            }
        else
        {
            n = name;
            p = power;
            h = health;
        }



        while (h > 0)
        {
                //This while loop shows that while the enemy health is greater than 0 this box will continue to show what the enemy attack and health is at the top,
                //the Middle area inside the box is showing you the options you hav. Underneath the box it displays your current stats such as how many potions you have, what 
                //your health is currently at and even how many coins you have. 
            Console.Clear();
            Console.WriteLine(n);
            Console.WriteLine("Attack Power " + p + " / Health " + h);
            Console.WriteLine("=====================================");
            Console.WriteLine("||    (A)ttack         (D)efend    ||");
            Console.WriteLine("||    (R)un            (H)eal      ||");
            Console.WriteLine("=====================================");
            Console.WriteLine("Current Potions: " + Program.currentPlayer.potion + " Current Health: " + Program.currentPlayer.health);
            Console.WriteLine("Current Coins: " + Program.currentPlayer.coins);
            ConsoleKey input = Console.ReadKey(true).Key;//The read key is used to accept whichever of the 4 Keys are pressed(A,D,R or H)
                if (input == ConsoleKey.A)  // || input.ToLower() == "attack")
            {
                //Attack
                Console.WriteLine("You relentlessly strike! As you strike, the " + n + " strikes you!");// these if /else if statements show what each button will do, like A will
                    //attack and deal what ever your current attack power is set to. You will also take some damage from whatever enemy as well. 
                Console.Beep(500, 500);//This is the noise for when you hit an enemy
                int damage = p - Program.currentPlayer.armorValue;// enemy power(attack)- the current players armorvalue... all of this will scale over time and as you buy items
                    //from the shop
                if (damage < 0)
                    damage = 0;
                int attack = Program.currentPlayer.weaponValue;
                Console.WriteLine("You lose " + damage + " health and deal " + attack + " damage!");
                Program.currentPlayer.health -= damage;
                h -= attack;
            }
            else if (input == ConsoleKey.D) // || input.ToLower() == "defend")
            {
                //Defend
                Console.WriteLine("As the " + n + " prepares to strikes you, you ready your wooden cross in a defensive stance!");
                Console.Beep(300, 500);
                int damage = (p / 2) - Program.currentPlayer.armorValue;
                if (damage < 0)
                    damage = 0;
                int attack = rand.Next(0, Program.currentPlayer.weaponValue) / 2;
                Console.WriteLine("You lose " + damage + " health and deal " + attack + " damage!");
                Program.currentPlayer.health -= damage;
                h -= attack;

            }
            else if (input == ConsoleKey.R)// || input.ToLower() == "run")
            {
                //Run
                if (rand.Next(0, 2) == 0)
                {
                    Console.WriteLine("As you try to sprint away from the " + n + ", its strike catches you in the back, sending you sprawling onto the ground!");
                    int damage = p - Program.currentPlayer.armorValue;
                    if (damage < 0) damage = 0;
                    Program.currentPlayer.health -= damage;
                    Console.ReadKey(true);
                    Console.WriteLine("You lose " + damage + " health and are unable to escape!");

                }
                else
                {
                    
                    Console.WriteLine("You use your agileness to evade the " + n + " and successfully escape!");
                    Console.ReadKey(true);
                    Shop.LoadShop(Program.currentPlayer);

                    //still need to add in what happens if you evade an attack and run away
                    //Trying to add a go to store option
                }
            }
            else if (input == ConsoleKey.H) //" || input.ToLower() == "heal")
            {
                //.currentPlayer.potion=rand.Next(0, 2);
                //Heal
                if (Program.currentPlayer.potion == 0)
                {
                    Console.WriteLine("As you desperatly grasp for a potion in your bag, all that you feel are empty glass flasks.");
                    Console.Beep(700, 300);
                    Console.Beep(600, 300);
                    Console.Beep(250, 900);
                    int damage = p - Program.currentPlayer.armorValue;
                    if (damage < 0)
                        damage = 0;
                    Program.currentPlayer.health -= damage;
                    Console.WriteLine("The " + n + " strikes you and you lose " + damage + " health!");
                    //Console.WriteLine("Press enter to continue.");

                }
                if (Program.currentPlayer.potion >= 1)
                {
                    Console.WriteLine("You reach into your bag and pull out a glowing red flask. You take a drink.");
                    int potion = 5;
                    Console.WriteLine("You gain " + potion + " health!");
                    Program.currentPlayer.health += potion;
                    Console.Beep(800, 700);
                    Console.Beep(800, 700);
                    Console.WriteLine("As you were occupied, the " + n + " struck you!");
                    Program.currentPlayer.potion = Program.currentPlayer.potion - 1;
                    int damage = (p/2) - Program.currentPlayer.armorValue;
                    if (damage < 0)
                        damage = 0;
                    Program.currentPlayer.health -= damage;
                    Console.WriteLine("You lose " + damage + " health!");
                }

            }

            //Death code
            if (Program.currentPlayer.health <= 0)
            {// we cant have a player go negative in game so we have to set a death code. So if the player Hp hits 0 the game will spit out the last string saying you die and then 
                    //end the program
                Console.WriteLine();
                Console.Write("As you lay bleeding out on the floor ");
                Console.WriteLine("the " + n + " hovers over your body for a moment before delivering ");
                Console.Write("the final blow, ending your life...");
                Console.Beep(700, 300);
                Console.Beep(600, 300);
                Console.Beep(500, 300);//These beeps will play when you die
                Console.Beep(400, 300);
                Console.Beep(300, 900);
                Console.ReadKey();
                System.Environment.Exit(0);
            }
            Console.ReadKey(true);
        }
        int c = Program.currentPlayer.GetCoins();
        Console.WriteLine("As you stand victorius over the " + n + ", its body dissolves into " + c + " gold coins!");
        Console.Beep(659, 125); Console.Beep(659, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(167); Console.Beep(523, 125); Console.Beep(659, 125);
        Thread.Sleep(125); Console.Beep(784, 125); Thread.Sleep(375); Console.Beep(392, 125); Thread.Sleep(375);
        Program.currentPlayer.coins += c;
        Console.WriteLine("Press enter");
        Console.ReadKey();
    }// If you dont die the above code lets you receive coins, writes a string saying you beat this enemy and plays a tune to signify you beating something...The next encounter
        //comes after pressing enter

    public static string GetName()
    {
        switch (rand.Next(0, 4))//This is the method  GetName that gives you the 4(5) possible encounters that you can have 
        {
            case 0:
                return "Guy named Tivo";
            case 1:
                return "Zombie";
            case 2:
                return "Human Cultist";
            case 3:
                return "Scrawny Orc";
        }
        return "Human Rogue";
    }
}
}
