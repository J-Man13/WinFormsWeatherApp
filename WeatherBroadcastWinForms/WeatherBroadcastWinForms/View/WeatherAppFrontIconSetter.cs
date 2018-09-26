using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeatherBroadcastWinForms.Model;

namespace WeatherBroadcastWinForms.View
{
    class WeatherAppFrontIconSetter
    {
        public static void SetWeatherIcon(PictureBox pictureBox, WeatherModel weatherModel)
        {
            string iconName = weatherModel.IconLink.Split('/')[5];

            string currentImagePath = @"c:\WeatherAppPicks\" + iconName;

            if (!System.IO.File.Exists(@"c:\WeatherAppPicks\" + iconName))
            {
                WebClient webClient = new WebClient();
                webClient.DownloadFile(weatherModel.IconLink, @"c:\WeatherAppPicks\" + iconName);
            }

            pictureBox.Image = Image.FromFile(currentImagePath);
        }
    }
}
