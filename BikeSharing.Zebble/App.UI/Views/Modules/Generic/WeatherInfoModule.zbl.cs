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
                locationText.Text = weatherInfo.LocationName;
                tempText.Text = $"{Math.Round(weatherInfo.Temp)}˚{weatherInfo.TempUnitShort}";
                dateText.Text = LocalTime.Now.ToString("dddd, MMMM dd");
            }
        }
    }
}