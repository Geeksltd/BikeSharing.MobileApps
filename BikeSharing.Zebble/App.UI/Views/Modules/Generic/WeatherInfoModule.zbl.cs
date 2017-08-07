namespace UI.Modules
{
    using Domain;
    using Domain.Services;
    using System;
    using System.Threading.Tasks;
    using static Domain.Services.Api;

    partial class WeatherInfoModule
    {

        public override async Task OnInitializing()
        {
            await base.OnInitializing();
            await InitializeComponents();
            var weather = await OpenWeatherMapService.GetDemoWeatherInfoAsync();

            if (weather is WeatherInfo)
            {
                var weatherInfo = weather as WeatherInfo;
                Location.Text = weatherInfo.LocationName;
                Temp.Text = $"{Math.Round(weatherInfo.Temp).ToString()}˚{weatherInfo.TempUnitShort}";
                Date.Text = DateTime.Now.ToString("dddd, MMMM dd");
            }
        }
    }
}