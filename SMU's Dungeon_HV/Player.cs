using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMU_s_Dungeon_HV
{
    [Serializable]
    public class Player
    {//this class sets the players basic stats such as starting coins, health, damage, etc. For the example we are giving the player 10000 coins to show how the game can scale
        // when you buy stuff from the shop
        Random rand=new Random();

        public string name;
        public int id;
        public int coins =10000;
        public int health = 10;
        public int damage = 1;
        public int armorValue = 0;
        public int potion = 5;
        public int weaponValue = 1;

        public int mods=0;

        public int GetHealth()
        {//this is the modifier for when you buy health
            int upper = (2 * mods+5);
            int lower = (mods+2);
            return rand.Next(lower,upper);
        }
        public int GetPower()
        {
            //this is the modifier for when you buy power(attack)
            int upper = (2 * mods + 3);
            int lower = (mods + 1);
            return rand.Next(lower, upper);
        }
        public int GetCoins()
        {
            //this is the modifier for when you increase difficulty and will adjust the amount of coins that will drop from an enemy.

            int upper = (15 * mods + 50);//add 15 with highest being 50
            int lower = (10 * mods + 10);//add 10 with lowest being 10
            return rand.Next(lower, upper);
        }
    }
}
