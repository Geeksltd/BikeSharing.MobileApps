using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using Domain.Entities;
using UI;
using Zebble;

namespace Domain.Services
{
    public class OpenWeatherMapService : BaseApi
    {
        const int OkResponseCode = 200;

        public async Task<WeatherInfo> GetWeatherInfoAsync()
        {
            var location = await Device.Location.GetCurrentPosition();

            if (location != null)
            {
                var builder = new UriBuilder(string.Format("{0}data/2.5/weather", GlobalSettings.OpenWeatherMapEndpoint))
                {
                    Query = $"lat={location.Latitude}&lon={location.Longitude}&units=imperial&appid={GlobalSettings.OpenWeatherMapAPIKey}"
                };
                var uri = builder.ToString();

                var response = await BaseApi.Get<OpenWeatherMapResponse>(uri);
                if (response?.cod == OkResponseCode)
                {
                    var weatherInfo = new WeatherInfo
                    {
                        LocationName = response.name,
                        Temp = response.main.temp,
                        TempUnit = TempUnit.Fahrenheit
                    };

                    return weatherInfo;
                }

                Device.Log.Error("OpenWeatherMap API answered with: " + ((response != null) ? $"Error code = {response.cod}." : "Invalid response."));
            }

            // Default data for demo
            return new WeatherInfo
            {
                LocationName = GlobalSettings.City,
                Temp = GlobalSettings.Temp,
                TempUnit = TempUnit.Fahrenheit
            };
        }

        public async Task<WeatherInfo> GetDemoWeatherInfoAsync()
        {
            var geolocation = new GeoLocation(GlobalSettings.EventLatitude, GlobalSettings.EventLongitude);

            var latitude = geolocation.Latitude.ToString("0.0000", CultureInfo.InvariantCulture);
            var longitude = geolocation.Longitude.ToString("0.0000", CultureInfo.InvariantCulture);

            var builder = new UriBuilder(string.Format("{0}data/2.5/weather", GlobalSettings.OpenWeatherMapEndpoint))
            {
                Query = $"lat={latitude}&lon={longitude}&units=imperial&appid={GlobalSettings.OpenWeatherMapAPIKey}"
            };
            var uri = builder.ToString();

            var response = await BaseApi.Get<OpenWeatherMapResponse>(uri);

            if (response?.cod == OkResponseCode)
            {
                var weatherInfo = new WeatherInfo
                {
                    LocationName = GlobalSettings.City,
                    Temp = response.main.temp,
                    TempUnit = TempUnit.Fahrenheit
                };

                return weatherInfo;
            }

            Device.Log.Error("OpenWeatherMap API answered with: " + ((response != null) ? $"Error code = {response.cod}." : "Invalid response."));

            // Default data for demo
            return new WeatherInfo
            {
                LocationName = GlobalSettings.City,
                Temp = GlobalSettings.Temp,
                TempUnit = TempUnit.Fahrenheit
            };
        }
    }
}
