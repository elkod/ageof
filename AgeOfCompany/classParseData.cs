using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgeOfCompany
{
    public class classParseData
    {
        Boolean needPlayerSearch = true;
        int playerCount = 0;
        int time = -1;
        int playerId = 0;
        public void parse()
        {
            try
            {
                StreamReader reader = File.OpenText("GameData.txt");
                string line;

                reader.ReadLine();
                PlayerDataAndTime timeData = null;
                while ((line = reader.ReadLine()) != null)
                {
              
                    string[] items = line.Split(',');

                    if (needPlayerSearch)
                        checkNewPlayer(items);


                    if(time!= int.Parse(items[0]))//new time begin
                    {
                        if (timeData != null)
                            GameData.gameData.Add(timeData);
                        playerId = 0;

                        time = int.Parse(items[0]);
                        timeData = new PlayerDataAndTime();
                        timeData.time = time;
                    }


                    timeData.playerData.Add(new PlayerData(playerId++, int.Parse(items[2]), int.Parse(items[3]), int.Parse(items[4]), int.Parse(items[5]), int.Parse(items[6]), int.Parse(items[7])));
                }
               // GameData.playerNameAndID.Clear();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        private void checkNewPlayer(string[] items)
        {
            //check this player exsist
            foreach(PlayerNameAndID element in GameData.playerNameAndID)
            {
                if(element.name.Equals(items[1]))
                {
                    //Yes player exsist.After than never search new player.
                    needPlayerSearch = false;
                    return;
                }
            }

            //add new player
            PlayerNameAndID player=new PlayerNameAndID();
            player.id = playerCount++;
            player.name = items[1];
            GameData.playerNameAndID.Add(player);
        }
    }
}
