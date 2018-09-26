using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherBroadcastWinForms.Model;
using WeatherBroadcastWinForms.Services;

namespace WeatherBroadcastWinForms.Presenter
{
    class WeatherAppFrontPresenter
    {
        
        public static IEnumerable<WeatherModel> GetWeatherModels(string city)
        {
            WeaterModelServices weaterModelServices = new WeaterModelServices();
            return weaterModelServices.GetWeatherModels(city); 
        }
        
        public static WeatherModel GetAverageWeatherModelPerDay(LinkedList<WeatherModel> weatherModels)
        {
            int averageMaxTemp = 0;
            int averageMinTemp = 0;
            int averageHumidity = 0;
            double averageWind = 0;
            double averagePressure = 0;
            string averageLink = "";
            int averageLinkCounter = 0;
            string averageStatus = "";
            int averageStatusCounter = 0;

            for (int i = 0 ; i < weatherModels.Count; i++)
            {
                averageMaxTemp += weatherModels.ElementAt(i).MaxTemp;
                averageMinTemp += weatherModels.ElementAt(i).MinTemp;
                averageWind += weatherModels.ElementAt(i).WindSpeed;
                averageHumidity += weatherModels.ElementAt(i).Humidity;
                averagePressure += weatherModels.ElementAt(i).Pressure;
                
                string link = weatherModels.ElementAt(i).IconLink;
                int linkCounter = 0;
                string status = weatherModels.ElementAt(i).Status;
                int statusCounter = 0;
                for(int j = i+1; j < weatherModels.Count; j++)
                {
                    if (link.Equals(weatherModels.ElementAt(j).IconLink))
                        linkCounter++;
                    if (status.Equals(weatherModels.ElementAt(j).Status))
                        statusCounter++;
                }
                if(linkCounter > averageLinkCounter) {
                    averageLinkCounter = linkCounter;
                    averageLink = link;
                }
                if (statusCounter > averageStatusCounter)
                {
                    averageStatusCounter = statusCounter;
                    averageStatus = status;
                }
            }
            averageMaxTemp = averageMaxTemp / weatherModels.Count;
            averageMinTemp = averageMinTemp / weatherModels.Count;
            averageWind = Double.Parse((averageWind / weatherModels.Count).ToString("#.##"));
            averageHumidity = averageHumidity / weatherModels.Count;
            averagePressure = Double.Parse((averagePressure / weatherModels.Count).ToString("#.##"));

            return new WeatherModel
            {
                Date = weatherModels.ElementAt(0).Date,
                MaxTemp = averageMaxTemp,
                MinTemp = averageMinTemp,
                WindSpeed = averageWind,
                Humidity = averageHumidity,
                Pressure = averagePressure,
                Status = averageStatus,
                IconLink = averageLink
            };
            
        }

    }
}
