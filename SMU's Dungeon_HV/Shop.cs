using Smu_s_Dungeon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMU_s_Dungeon_HV
{
     public class Shop
    {
        public static void LoadShop(Player p)
        {
            RunShop(p);
        }

        public static void RunShop(Player p)
        {
            int potionP;
            int armorP;
            int weaponP;
            int difP;
            int store;
            while (true)
            {//while loop for when a player is currently using the shop it shows below that when you purchase armor, weapons or difficulty it will increase in price everytime
                //the potions you can buy will increase in cost as you buy mods(difficulty)
                potionP = 10 + 10 * p.mods;
                armorP = 50 * (p.armorValue+1);//"item"P= how much it costs
                weaponP = 50 * p.weaponValue;
                difP = 300 + 100 * p.mods;
                Console.Clear();
                Console.WriteLine("     0==0============================0==0      ");
                Console.WriteLine("    0==0                              0==0    ");
                Console.WriteLine("   0==0           ***SHOP***           0==0   ");
                Console.WriteLine("  0==0                                  0==0  ");
                Console.WriteLine("    =======================================   ");
                Console.WriteLine("    ||          (W)eapon: $" + weaponP +"            "+"||");
                Console.WriteLine("    ||          (A)rmor: $" + armorP + "             "+        "||");
                Console.WriteLine("    ||          (P)otions: $" + potionP +"           " +"|| ");
                Console.WriteLine("    ||         (D)ifficulty: $" + difP + "        "+ "||");
                Console.WriteLine("    =======================================   ");
                Console.WriteLine("                     (E)xit                  ");
                Console.WriteLine("                     (Q)uit                  ");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("                " + p.name + "'s Stats");
                Console.WriteLine("        ======================================");
                Console.WriteLine("             Current Health: " + p.health);
                Console.WriteLine("             Coins:          "+p.coins);
                Console.WriteLine("             Weapon Strength: " + p.weaponValue);
                Console.WriteLine("             Armor toughness: " + p.armorValue);// this box shows all current stats for the player before returning to the real game.
                Console.WriteLine("             Potions: " + p.potion);
                Console.WriteLine("             Difficulty Mods: " + p.mods);
                Console.WriteLine("        ======================================");
                //Wait for input
                Console.WriteLine();
                Console.WriteLine("To exit and return to game press 'E' and then enter");
                ConsoleKey input = Console.ReadKey(true).Key;
                if (input == ConsoleKey.W)
                {
                    TryBuy("weapon", weaponP, p);
                }
                else if (input == ConsoleKey.A)
                {
                    TryBuy("armor", armorP, p);

                }
                else if (input == ConsoleKey.P)
                {
                    TryBuy("potion", potionP, p);

                }
                else if (input == ConsoleKey.D)
                {
                    TryBuy("dif", difP, p);

                }
                else if (input == ConsoleKey.Q)
                {
                    Program.Quit();
                }            

                else if (input == ConsoleKey.E)
                    break;
            }
        }
    
        static void TryBuy(string item, int cost, Player p)
        {
            if (p.coins >= cost)
            {
                if (item == "potion")
                    p.potion++;
                else if (item == "weapon")
                    p.weaponValue++;
                else if (item == "armor")
                    p.armorValue++;
                else if (item == "dif")
                    p.mods++;

                p.coins -= cost;
            }
            else
            {
                Console.WriteLine("You aint got enough coin!");
                Console.ReadKey(true);
            }
        }
        }
    }

