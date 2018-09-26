using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherBroadcastWinForms.Model;

namespace WeatherBroadcastWinForms.Services
{
    interface IGetWeatherModels
    {
        IEnumerable<WeatherModel> GetWeatherModels(string city); 
    }
}
