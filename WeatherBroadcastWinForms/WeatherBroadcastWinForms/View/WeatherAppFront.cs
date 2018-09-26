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
using WeatherBroadcastWinForms.Model;
using WeatherBroadcastWinForms.Presenter;
using WeatherBroadcastWinForms.View;

namespace WeatherBroadcastWinForms
{
    public partial class WeatherAppFront : Form
    {
        private bool isCelsium;
        public WeatherAppFront()
        {
            InitializeComponent();
            if (!System.IO.Directory.Exists(@"c:\WeatherAppPicks"))
                System.IO.Directory.CreateDirectory(@"c:\WeatherAppPicks");

            try
            {
                string searchButtonImagePath = @"c:\WeatherAppPicks\search.png";
                if (!System.IO.File.Exists(searchButtonImagePath))
                {
                    WebClient webClient = new WebClient();
                    webClient.DownloadFile("http://icons-for-free.com/free-icons/png/512/254211.png", searchButtonImagePath);
                }
                buttonSearch.BackgroundImage = Image.FromFile(searchButtonImagePath);
                textBoxSearch.Text = "Baku";
                getWeaterData();
            }
            catch (Exception e)
            {
                MessageBox.Show("No internet connection");
                makeInvisible();
            }
            isCelsium = true;
        }

        private void textBoxSearch_Click(object sender, EventArgs e)
        {
            textBoxSearch.Text = "";
        }

        private void textBoxSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    getWeaterData();
                    makeVisible();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("No internet connection or invalid search pattern");
                makeInvisible();
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            try {
                getWeaterData();
                makeVisible();
            }
            catch(Exception )
            {
                MessageBox.Show("No internet connection or invalid search pattern");
                makeInvisible();
            }
        }

        private void getWeaterData()
        {
            if (String.IsNullOrEmpty(textBoxSearch.Text))
                throw new NullReferenceException();
            string city = textBoxSearch.Text;
            LinkedList<WeatherModel> weatherModels = new LinkedList<WeatherModel>();          
                
            WeatherAppFrontPresenter.GetWeatherModels(city).ToList().ForEach((i)=> weatherModels.AddLast(i));
            WeatherAppFrontBroadCastByDays.GetCurrentWeatherDate(this,weatherModels.ElementAt(0));

            LinkedList<WeatherModel> weatherModels1 = new LinkedList<WeatherModel>();
            for (int i = 1; i < 8; i++)
                weatherModels1.AddLast(weatherModels.ElementAt(i));
            WeatherAppFrontBroadCastByDays.GetHourlytWeatherdate(this,weatherModels1);

            LinkedList<WeatherModel> weatherModels2 = new LinkedList<WeatherModel>();
            for (int i = 8; i < weatherModels.Count; i++)
                weatherModels2.AddLast(weatherModels.ElementAt(i));
            WeatherAppFrontBroadCastByDays.GetDailytWeatherdate(this,weatherModels2, weatherModels.ElementAt(0).Date);           
        }
        

        private void buttonC_Click(object sender, EventArgs e)
        {
            isCelsium = WeatherAppFrontButtonsProcessor.buttonC_Click(this, isCelsium);
        }

        private void buttonF_Click(object sender, EventArgs e)
        {
            isCelsium = WeatherAppFrontButtonsProcessor.buttonF_Click(this, isCelsium);
        }

        public void makeInvisible()
        {
            foreach (Control c in this.Controls)
                if (c.Name != "panel3")
                    c.Visible = false;
        }
        public void makeVisible()
        {
            foreach (Control c in this.Controls)
                if (c.Name != "panel3")
                    c.Visible = true;
        }

    }
}
