using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgeOfCompany
{



    public static class GameData
    {
        public static List<PlayerDataAndTime> gameData = new List<PlayerDataAndTime>();
        public static List<PlayerNameAndID> playerNameAndID = new List<PlayerNameAndID>();
    }



    public class PlayerNameAndID//String condition take to much time.Every query or filter use integer type id variable.
    {
        public string name;
        public int id;
    }

    public class PlayerData
    {
        public int id,wood, food, gold, stone, vils, mils;
        public PlayerData(int id, int wood, int food, int gold, int stone, int vils, int mils)
        {
            this.id = id;
            this.wood = wood;
            this.food = food;
            this.gold = gold;
            this.stone = stone;
            this.vils = vils;
            this.mils = mils;
        }
        public double getPlayerScore()
        {
            return mils * 75 * 0.2 + vils * 50 * 0.2 + (food + wood + gold + stone) * 0.2;
        }     
    }

    public class PlayerDataAndTime
    {
        public int time;
        public List<PlayerData> playerData = new List<PlayerData>();
    }





}
