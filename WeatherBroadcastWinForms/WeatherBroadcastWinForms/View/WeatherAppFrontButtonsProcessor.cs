using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeatherBroadcastWinForms.View
{
    class WeatherAppFrontButtonsProcessor
    {
        public static bool buttonC_Click(WeatherAppFront weatherAppFront,bool isCelsium)
        {   if (isCelsium == false)
            {
                foreach (Control ctrl in weatherAppFront.Controls)
                    if (ctrl is TableLayoutPanel)
                        foreach (Control tableControl in (ctrl as TableLayoutPanel).Controls)
                            if (tableControl.Name.Contains("labelTemp"))
                                tableControl.Text = (int)Math.Round((Int32.Parse(tableControl.Text.Split('°')[0]) - 32) * 0.55555555555) + "°C";
                weatherAppFront.labelTempCurrent.Text = (int)Math.Round((Int32.Parse(weatherAppFront.labelTempCurrent.Text.Split('°')[0]) - 32) * 0.55555555555) + "°C";
                return true;
            }
            else
            {
                return true;
            }
        }

        public static bool buttonF_Click(WeatherAppFront weatherAppFront, bool isCelsium)
        {
            if (isCelsium == true)
            {
                foreach (Control ctrl in weatherAppFront.Controls)
                    if (ctrl is TableLayoutPanel)
                        foreach (Control tableControl in (ctrl as TableLayoutPanel).Controls)
                            if (tableControl.Name.Contains("labelTemp"))
                                tableControl.Text = (Int32)((Int32.Parse(tableControl.Text.Split('°')[0]) * 9) / 5) + 32 + "°F";
                weatherAppFront.labelTempCurrent.Text = (Int32)((Int32.Parse(weatherAppFront.labelTempCurrent.Text.Split('°')[0]) * 9) / 5) + 32 + "°F";
                return false;
            }
            else
            {
                return false;
            }
            
        }
    }
}
