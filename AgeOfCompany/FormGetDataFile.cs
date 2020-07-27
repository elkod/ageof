using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgeOfCompany
{
    public partial class FormGetDataFile : Form
    {
        public FormGetDataFile()
        {
            InitializeComponent();
        }

        private void buttonDownload_Click(object sender, EventArgs e)
        {
            if (textBoxURL.Text == "")
                MessageBox.Show("Lütfen indirilecek dosyanın http adresini yazınız.");
            else
            {
                startDownload(textBoxURL.Text);

            }
        }
        private void startDownload(string URL)
        {
            try
            {
                buttonDownload.Enabled = false;
                WebClient webClient = new WebClient();
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
                webClient.DownloadFileAsync(new Uri(URL), "GameData.txt");
            }
            catch(Exception e)
            {
                MessageBox.Show("Indirilirken hata olustu!  "+e.Message);
                buttonDownload.Enabled = true;
            }
        }

        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            labelPercent.Text = "%"+e.ProgressPercentage.ToString();
        }

        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show("URL hatasi indirilemedi.");
                buttonDownload.Enabled = true;
            }
            else
            {
                buttonDownload.Enabled = true;
                classParseData parser = new classParseData();
                parser.parse();//parse text data to structer
                FormViewPlay frm = new FormViewPlay();
                frm.Show();
            }
        }

    }

}

