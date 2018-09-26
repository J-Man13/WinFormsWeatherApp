using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeatherBroadcastWinForms.Model;

namespace WeatherBroadcastWinForms.Services
{
    class WeaterModelServices:IGetWeatherModels
    {
        private static string URL = "http://api.openweathermap.org/data/2.5/forecast?q=";
        private static string PRIVATE_KEY = "&APPID=10adb849e6ad5e1da631c8a7b1f8fee1";



        public IEnumerable<WeatherModel> GetWeatherModels(string city)
        {
            JObject jObject;
            try
            {
                jObject = GetJObject(city);
            }
            catch (System.Net.WebException)
            {
                return null;
            }
            JToken jToken = jObject["list"];
            LinkedList<WeatherModel> linkedList = new LinkedList<WeatherModel>();
            foreach(JToken j in jToken)
            {
                WeatherModel weatherModel = new WeatherModel();

                JToken jMain = j["main"];
                DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                dateTime = dateTime.AddSeconds(Double.Parse(j["dt"].ToString()));
                weatherModel.Date = dateTime;    
                weatherModel.MinTemp =(Int32)Double.Parse(jMain["temp_min"].ToString())-273;
                weatherModel.MaxTemp = (Int32)Double.Parse(jMain["temp_max"].ToString())-273;
                weatherModel.Pressure = Double.Parse(jMain["pressure"].ToString());
                weatherModel.Humidity = (Int32)Double.Parse(jMain["humidity"].ToString());

                JToken jWeather = j["weather"];
                JToken jWeatherTrue = jWeather[0];
                weatherModel.Status = jWeatherTrue["main"].ToString();
                weatherModel.IconLink = "http://openweathermap.org/img/w/"+ jWeatherTrue["icon"].ToString() + ".png";

                JToken jWind = j["wind"];
                weatherModel.WindSpeed = Double.Parse(jWind["speed"].ToString());

                linkedList.AddLast(weatherModel);
            }
            return linkedList;
        }
        
        private JObject GetJObject(string city)
        {
            WebClient webClient = new WebClient();
            string jsonString = webClient.DownloadString(URL + city + PRIVATE_KEY);
            return JsonConvert.DeserializeObject(jsonString) as JObject;
        }
    }
}
