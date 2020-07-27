using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace AgeOfCompany
{
    public partial class FormViewPlay : Form
    {
        int index = 1;
        public FormViewPlay()
        {
            InitializeComponent();
        }

        private void FormViewPlay_Load(object sender, EventArgs e)
        {
            this.chart1.Series.Clear();
            comboBoxSpeed.SelectedIndex=0;

            foreach (PlayerNameAndID player in GameData.playerNameAndID)
            {
                comboBoxPlayers.Items.Add(player.name);
            }

            comboBoxPlayers.Items.Add("ALL PLAYER");
            comboBoxPlayers.SelectedIndex = comboBoxPlayers.Items.Count-1;
        }

        private void addSeries(string name, SeriesChartType tur)
        {
            Series series = this.chart1.Series.Add(name);
            series.ChartType = tur;
            series.BorderWidth = 3;
            series.IsValueShownAsLabel = true;
            listSeries.Add(series);

        }
        List<Series> listSeries = new List<Series>();

        public void addValue(int playerID, string playerName,int time,double value)
        {
            if (comboBoxPlayers.SelectedIndex != comboBoxPlayers.Items.Count - 1)
            {
                if (playerID != comboBoxPlayers.SelectedIndex)
                    return;
            }

            repeat:
                bool finded = false;
                foreach (Series ser in listSeries)
                {
                    if (ser.Name.Equals(playerName))
                    {
                        ser.Points.AddXY(time,value);
                        if(ser.Points.Count()>50)
                        {
                            ser.Points.RemoveAt(0);
                        }
                        finded = true;

                    }
                }
                if (!finded)
                {
                    addSeries(playerName, SeriesChartType.Spline);
                    goto repeat;
                }
            chart1.ChartAreas[0].RecalculateAxesScale();
        }

      
        private void timer1_Tick(object sender, EventArgs e)
        {
            listBoxPlayerData.Items.Clear();
            listBoxPlayerData.Items.Add("------- Time:"+ GameData.gameData[index].time.ToString() + "------");
            foreach (PlayerData element in GameData.gameData[index++].playerData)
            {
                addValue(GameData.playerNameAndID[element.id].id,GameData.playerNameAndID[element.id].name, GameData.gameData[index].time,element.getPlayerScore());
                listBoxPlayerData.Items.Add(" wood:"+ element.wood+ " food:" + element.food  + " gold:" + element.gold + " stone:" + element.stone + " vils:" + element.vils + " mils:" + element.mils + " SCORE:" + element.getPlayerScore().ToString() + " " + GameData.playerNameAndID[element.id].name);
            }
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled)
            {
                index = 1;
                this.chart1.Series.Clear();
                listSeries.Clear();
                timer1.Enabled = true;
                buttonPlay.Text = "Stop";
                comboBoxPlayers.Enabled = false;
            }
            else
            {
                buttonPlay.Text = "Play";
                timer1.Enabled = false;
                comboBoxPlayers.Enabled = true;
               

            }
        }

        private void comboBoxSpeed_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxSpeed.SelectedIndex == 0)
                timer1.Interval = 1000;
            else if (comboBoxSpeed.SelectedIndex == 1)
                timer1.Interval = 1000 / 2;
            else if (comboBoxSpeed.SelectedIndex == 2)
                timer1.Interval = 1000 / 4;
            else if (comboBoxSpeed.SelectedIndex == 3)
                timer1.Interval = 1000 / 16;
        }
    }
}
