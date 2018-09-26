using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeatherBroadcastWinForms.Model;
using WeatherBroadcastWinForms.Presenter;

namespace WeatherBroadcastWinForms.View
{
    class WeatherAppFrontBroadCastByDays
    {
        public static void GetCurrentWeatherDate(WeatherAppFront weatherAppFront, WeatherModel weatherModel)
        {
            weatherAppFront.labelDateCurrent.Text = weatherModel.Date.DayOfWeek.ToString() + " - " + weatherModel.Date.ToString("yyyy.MM.dd");
            weatherAppFront.labelTempCurrent.Text = (((weatherModel.MaxTemp + weatherModel.MinTemp) / 2)).ToString() + "°C";
            weatherAppFront.labelCurrentWeaterStatus.Text = weatherModel.Status;
            weatherAppFront.labelCurrentHumidity.Text = "Humidity : " + weatherModel.Humidity + "%";
            weatherAppFront.labelCurrentWind.Text = "Wind : " + weatherModel.WindSpeed + " m/s";
            weatherAppFront.labelCurrentPressure.Text = "Pressure : " + weatherModel.Pressure + " hPa";
            WeatherAppFrontIconSetter.SetWeatherIcon(weatherAppFront.pictureBoxCurrent, weatherModel);
        }

        public static void GetHourlytWeatherdate(WeatherAppFront weatherAppFront, LinkedList<WeatherModel> weatherModels)
        {
            LinkedList<Control> labelsHourTime = new LinkedList<Control>();
            LinkedList<Control> pictureBoxesHourIcon = new LinkedList<Control>();
            LinkedList<Control> labelsHourTemp = new LinkedList<Control>();
            LinkedList<Control> labelsHourStatus = new LinkedList<Control>();


            foreach (Control ctrl in weatherAppFront.tableLayoutPanel5.Controls)
            {
                if (ctrl is Label)
                {
                    if (ctrl.Name.Contains("labelHourTime"))
                        labelsHourTime.AddLast(ctrl as Label);
                    if (ctrl.Name.Contains("labelTempHour"))
                        labelsHourTemp.AddLast(ctrl as Label);
                    if (ctrl.Name.Contains("labelHourStatus"))
                        labelsHourStatus.AddLast(ctrl as Label);
                }
                else if (ctrl is PictureBox)
                {
                    pictureBoxesHourIcon.AddLast(ctrl as PictureBox);
                }
            }
            labelsHourTime = SortControlByName(labelsHourTime);
            pictureBoxesHourIcon = SortControlByName(pictureBoxesHourIcon);
            labelsHourTemp = SortControlByName(labelsHourTemp);
            labelsHourStatus = SortControlByName(labelsHourStatus);

            for (int i = 0; i < weatherModels.Count; i++)
            {
                labelsHourTime.ElementAt(i).Text = weatherModels.ElementAt(i).Date.ToString("HH:mm");
                WeatherAppFrontIconSetter.SetWeatherIcon(pictureBoxesHourIcon.ElementAt(i) as PictureBox, weatherModels.ElementAt(i));
                labelsHourTemp.ElementAt(i).Text = ((weatherModels.ElementAt(i).MaxTemp + weatherModels.ElementAt(i).MinTemp) / 2).ToString() + "°C";
                labelsHourStatus.ElementAt(i).Text = weatherModels.ElementAt(i).Status;
            }
        }

        public static void GetDailytWeatherdate(WeatherAppFront weatherAppFront,LinkedList<WeatherModel> weatherModels, DateTime currentDateTime)
        {
            LinkedList<WeatherModel> weatherModels1 = new LinkedList<WeatherModel>();
            LinkedList<WeatherModel> weatherModels2 = new LinkedList<WeatherModel>();
            LinkedList<WeatherModel> weatherModels3 = new LinkedList<WeatherModel>();
            LinkedList<WeatherModel> weatherModels4 = new LinkedList<WeatherModel>();
            LinkedList<WeatherModel> weatherModels5 = new LinkedList<WeatherModel>();

            for (int i = 0; i < weatherModels.Count; i++)
            {

                if ((Int32)(weatherModels.ElementAt(i).Date - currentDateTime).TotalDays == 1)
                {
                    weatherModels1.AddLast(weatherModels.ElementAt(i));
                }
                else if ((Int32)(weatherModels.ElementAt(i).Date - currentDateTime).TotalDays == 2)
                {
                    weatherModels2.AddLast(weatherModels.ElementAt(i));
                }
                else if ((Int32)(weatherModels.ElementAt(i).Date - currentDateTime).TotalDays == 3)
                {
                    weatherModels3.AddLast(weatherModels.ElementAt(i));
                }
                else if ((Int32)(weatherModels.ElementAt(i).Date - currentDateTime).TotalDays == 4)
                {
                    weatherModels4.AddLast(weatherModels.ElementAt(i));
                }

            }

            WeatherModel averageWeatherModel1 = WeatherAppFrontPresenter.GetAverageWeatherModelPerDay(weatherModels1);
            WeatherModel averageWeatherModel2 = WeatherAppFrontPresenter.GetAverageWeatherModelPerDay(weatherModels2);
            WeatherModel averageWeatherModel3 = WeatherAppFrontPresenter.GetAverageWeatherModelPerDay(weatherModels3);
            WeatherModel averageWeatherModel4 = WeatherAppFrontPresenter.GetAverageWeatherModelPerDay(weatherModels4);
            LinkedList<WeatherModel> averageWeatherModels = new LinkedList<WeatherModel>();
            averageWeatherModels.AddLast(averageWeatherModel1);
            averageWeatherModels.AddLast(averageWeatherModel2);
            averageWeatherModels.AddLast(averageWeatherModel3);
            averageWeatherModels.AddLast(averageWeatherModel4);

            for (int i = 0; i < averageWeatherModels.Count; i++)
            {
                foreach (Control ctrl in weatherAppFront.tableLayoutPanel1.Controls)
                {
                    if (ctrl is Label)
                    {
                        if (ctrl.Name.Contains("labelDailyDate"))
                        {
                            if (ctrl.Name.Contains((i + 1).ToString()))
                                ctrl.Text = averageWeatherModels.ElementAt(i).Date.ToString("yyyy.MM.dd");
                        }
                        else if (ctrl.Name.Contains("labelDailyDay"))
                        {
                            if (ctrl.Name.Contains((i + 1).ToString()))
                                ctrl.Text = averageWeatherModels.ElementAt(i).Date.DayOfWeek.ToString();
                        }
                        else if (ctrl.Name.Contains("labelTempDaily"))
                        {
                            if (ctrl.Name.Contains((i + 1).ToString()))
                                ctrl.Text = (averageWeatherModels.ElementAt(i).MaxTemp + averageWeatherModels.ElementAt(i).MinTemp) / 2 + "°C";
                        }
                        else if (ctrl.Name.Contains("labelWindDaily"))
                        {
                            if (ctrl.Name.Contains((i + 1).ToString()))
                                ctrl.Text = averageWeatherModels.ElementAt(i).WindSpeed + " m/s";
                        }
                    }
                    else if (ctrl is PictureBox)
                    {
                        if (ctrl.Name.Contains("pictureBoxDaily"))
                        {
                            if (ctrl.Name.Contains((i + 1).ToString()))
                                WeatherAppFrontIconSetter.SetWeatherIcon(ctrl as PictureBox, averageWeatherModels.ElementAt(i));
                        }
                    }
                }
            }
        }


        public static LinkedList<Control> SortControlByName(LinkedList<Control> controls)
        {
            LinkedList<Control> sortedControls = new LinkedList<Control>();
            controls.OrderBy(i => i.Name).ToList().ForEach(i => sortedControls.AddLast(i));
            return sortedControls;
        }
        
    }
}
