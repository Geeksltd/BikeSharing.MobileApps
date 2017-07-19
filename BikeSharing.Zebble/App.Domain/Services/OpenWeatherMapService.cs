using Domain.Entities;
using Domain.Enums;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using UI._Custom;
using Zebble.Framework;
using Zebble.Services;

namespace Domain.Services
{
    public class OpenWeatherMapService : BaseApi
    {
        private const int OkResponseCode = 200;
        private readonly string OpenWeatherMapEndpoint = "http://api.openweathermap.org/";

       // private readonly ILocationProvider _locationProvider;
       // private readonly IRequestProvider _requestProvider;

      //  public OpenWeatherMapService(LocationProvider locationProvider, RequestProvider requestProvider)
      //  {
      //      this._locationProvider = locationProvider;
      //      this._requestProvider = requestProvider;
       // }

        //public async Task<WeatherInfo> GetWeatherInfoAsync()
        //{
        //    var location = await this._locationProvider.GetPositionAsync();
        //    if (location is GeoLocation)
        //    {
        //        var geolocation = location as GeoLocation;
        //        var latitude = geolocation.Latitude.ToString("0.0000", CultureInfo.InvariantCulture);
        //        var longitude = geolocation.Longitude.ToString("0.0000", CultureInfo.InvariantCulture);

        //        var builder = new UriBuilder(OpenWeatherMapEndpoint);
        //        builder.Path = $"data/2.5/weather";
        //        builder.Query = $"lat={latitude}&lon={longitude}&units=imperial&appid={GlobalSettings.OpenWeatherMapAPIKey}";
        //        var uri = builder.ToString();

        //        var response = await Api.Get<OpenWeatherMapResponse>(uri);
        //        if (response?.cod == OkResponseCode)
        //        {
        //            var weatherInfo = new WeatherInfo
        //            {
        //                LocationName = response.name,
        //                Temp = response.main.temp,
        //                TempUnit = TempUnit.Fahrenheit
        //            };

        //            return weatherInfo;
        //        }

        //        Debug.WriteLine("OpenWeatherMap API answered with: " + ((response != null) ? $"Error code = {response.cod}." : "Invalid response."));
        //    }

        //    // Default data for demo
        //    return new WeatherInfo
        //    {
        //        LocationName = GlobalSettings.City,
        //        Temp = 56,
        //        TempUnit = TempUnit.Fahrenheit
        //    };
        //}

        public async Task<WeatherInfo> GetDemoWeatherInfoAsync()
        {
            var geolocation = new GeoLocation(GlobalSettings.EventLatitude, GlobalSettings.EventLongitude);

            var latitude = geolocation.Latitude.ToString("0.0000", CultureInfo.InvariantCulture);
            var longitude = geolocation.Longitude.ToString("0.0000", CultureInfo.InvariantCulture);

            var builder = new UriBuilder(OpenWeatherMapEndpoint);
            builder.Path = $"data/2.5/weather";
            builder.Query = $"lat={latitude}&lon={longitude}&units=imperial&appid={GlobalSettings.OpenWeatherMapAPIKey}";
            var uri = builder.ToString();

            var response = await Api.Get<OpenWeatherMapResponse>(uri);

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

            Debug.WriteLine("OpenWeatherMap API answered with: " + ((response != null) ? $"Error code = {response.cod}." : "Invalid response."));

            // Default data for demo
            return new WeatherInfo
            {
                LocationName = GlobalSettings.City,
                Temp = 56,
                TempUnit = TempUnit.Fahrenheit
            };
        }
    }
}
