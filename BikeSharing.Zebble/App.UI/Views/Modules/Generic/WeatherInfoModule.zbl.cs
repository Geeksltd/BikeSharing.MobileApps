namespace UI.Modules
{
    using Domain;
    using System;
    using System.Threading.Tasks;
    using static Domain.Services.Api;

    partial class WeatherInfoModule
    {
        public override async Task OnInitializing()
        {
            await base.OnInitializing();
          
            var weather = await OpenWeatherMapService.GetDemoWeatherInfoAsync();

            if (weather is WeatherInfo)
            {
                var weatherInfo = weather as WeatherInfo;
                LocationText.Text = weatherInfo.LocationName;
                TempText.Text = $"{Math.Round(weatherInfo.Temp)}˚{weatherInfo.TempUnitShort}";
                DateText.Text = LocalTime.Now.ToString("dddd, MMMM dd");
            }
        }
    }
}