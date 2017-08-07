namespace UI.Modules
{
    using System;
    using System.Threading.Tasks;
    using Domain;
    using Domain.Services;

    partial class WeatherInfoModule
    {

        public override async Task OnInitializing()
        {
            await base.OnInitializing();
            await InitializeComponents();
            var weather = await new OpenWeatherMapService().GetDemoWeatherInfoAsync();

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