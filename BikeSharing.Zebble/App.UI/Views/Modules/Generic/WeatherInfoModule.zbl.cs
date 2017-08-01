namespace UI.Modules
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Zebble;
     
    using Domain;
    using Domain.Entities;
    using Domain.Services;

    partial class WeatherInfoModule
    {

        public override async Task OnInitializing()
        {
            await base.OnInitializing();
            await InitializeComponents();
            var _weatherService = new OpenWeatherMapService();
            var weather = await _weatherService.GetDemoWeatherInfoAsync();

            if (weather is WeatherInfo)
            {
                var weatherInfo = weather as WeatherInfo;
                Location.Text = weatherInfo.LocationName;
                Temp.Text = string.Format("{0}˚{1}", Math.Round(weatherInfo.Temp).ToString(), weatherInfo.TempUnitShort);
                Date.Text = DateTime.Now.ToString("dddd, MMMM dd");
            }
        }
    }
}