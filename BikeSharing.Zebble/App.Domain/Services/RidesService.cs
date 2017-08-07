using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using UI;
using Zebble;

namespace Domain.Services
{
    class RidesService : BaseApi
    {

        static List<Suggestion> suggestions = StaticData.GetSuggestions();

        static int StationsCounter = 0;

        static Station[] stations = new Station[]
        {
            new Station
            {
                Name = "Alki Beach Park I",
                Slots = 22,
                Occupied = 4,
                Latitude = 47.5790791f,
                Longitude = -122.4136163f
            },
            new Station
            {
                Name = "Alki Beach Park II",
                Slots = 12,
                Occupied = 7,
                Latitude = 47.5743905f,
                Longitude = -122.4023376f
            },
            new Station
            {
                Name = "Alki Point Lighthouse",
                Slots = 15,
                Occupied = 5,
                Latitude = 47.5766275f,
                Longitude = -122.4217906f
            }
        };

        static List<Ride> rides = new List<Ride>
        {
            new Ride
            {
                EventId = 1,
                RideType = RideType.Event,
                Name = "Ride Cultural",
                Start = DateTime.Now.AddDays(-7),
                Stop = DateTime.Now.AddDays(-7),
                Duration = 3600,
                Distance = 19,
                From = Stations[0].Name,
                FromStation = Stations[0],
                To = Stations[2].Name,
                ToStation = Stations[2]
            },
            new Ride
            {
                RideType = RideType.Custom,
                Start = DateTime.Now.AddDays(-14),
                Stop = DateTime.Now.AddDays(-14),
                Duration = 2500,
                Distance = 8900,
                From = Stations[1].Name,
                FromStation = Stations[1],
                To = Stations[0].Name,
                ToStation = Stations[0]
            },
            new Ride
            {
                RideType = RideType.Suggestion,
                Start = DateTime.Now.AddDays(-14),
                Stop = DateTime.Now.AddDays(-14),
                Duration = 1800,
                Distance = 10100,
                From = Stations[2].Name,
                FromStation = Stations[2],
                To = Stations[1].Name,
                ToStation = Stations[1]
            }
        };

        public static List<Suggestion> Suggestions { get => Suggestions1; set => Suggestions1 = value; }
        public static List<Suggestion> Suggestions1 { get => suggestions; set => suggestions = value; }
        public static Station[] Stations { get => stations; set => stations = value; }
        public static List<Ride> Rides { get => rides; set => rides = value; }
        public Task<Booking> RequestBikeBooking(Station station, Event @event)
        {
            return BikeBooking(station, RideType.Event, @event.Id);
        }

        public Task<Booking> RequestBikeBooking(Station station, Suggestion suggestion)
        {
            return BikeBooking(station, RideType.Suggestion, suggestion.Id);
        }

        public Task<Booking> RequestBikeBooking(Station fromStation, Station toStation)
        {
            return BikeBooking(fromStation, RideType.Custom, 0);
        }

        async Task<Booking> BikeBooking(Station station, RideType type, int id)
        {
            await Task.Delay(500);

            var booking = new Booking
            {
                Id = 222,
                FromStation = station,
                ToStation = Stations[0],
                RegistrationDate = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddMinutes(3),
                EventId = id,
                BikeId = 2332,
                RideType = type
            };

            Settings.CurrentBookingId = booking.Id;

            return booking;
        }

        public async Task<Booking> GetBooking(int bookingId)
        {
            await Task.Delay(500);


            return new Booking
            {
                Id = bookingId,
                FromStation = Stations[0],
                ToStation = Stations[1],
                RegistrationDate = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddMinutes(3),
                EventId = 1,
                BikeId = 2332,
                RideType = RideType.Event
            };
        }

        public Task<Station> GetNearestStationTo(GeoLocation location)
        {
            var station = Stations[StationsCounter++ % Stations.Count()];

            return Task.FromResult(station);
        }

        public async Task<IEnumerable<Suggestion>> GetSuggestions()
        {
            await Task.Delay(200);

            return Suggestions;
        }

        public async Task<Ride[]> GetUserRides()
        {
            var userId = new AuthenticationService().GetCurrentUserId();
            var builder = new UriBuilder(string.Format("{0}api/rides/user/{1}", GlobalSettings.RidesEndpoint, userId));
            string uri = builder.ToString();
            return await BaseApi.Get<Ride[]>(uri, cacheChoice: Zebble.ApiResponseCache.Refuse);
        }

        public void RemoveCurrentBooking()
        {
            Settings.RemoveCurrentBookingId();
        }

        public async Task<IEnumerable<Station>> GetNearestStations()
        {
            await Task.Delay(200);

            return Stations;
        }

        public async Task<Station[]> GetNearestStationsTo(GeoLocation location)
        {
            try
            {
                const int count = 10;
                var builder = new UriBuilder(string.Format("{0}/api/stations/nearto?latitude={1}&longitude={2}&count={3}", GlobalSettings.RidesEndpoint, location.Latitude.ToString(CultureInfo.InvariantCulture), location.Longitude.ToString(CultureInfo.InvariantCulture), count));

                string uri = builder.ToString();

                return Stations = await BaseApi.Get<Station[]>(uri);
            }
            catch
            {
                await Task.Delay(200);

                return Stations;
            }
        }

        public async Task<Station> GetInfoForNearestStation()
        {
            await Task.Delay(500);

            return Stations.FirstOrDefault();
        }

        public async Task<Station> GetInfoForNearestStationTo(GeoLocation toGeoLocation)
        {
            await Task.Delay(500);

            return Stations.FirstOrDefault();
        }

        public async Task<Station> GetStation(int stationId)
        {
            await Task.Delay(500);

            return Stations[stationId > 3 ? 0 : stationId];
        }

        public Task<Booking> RequestBikeBooking(Station fromStation, Station toStation, Event @event)
        {
            return BikeBooking(fromStation, RideType.Event, @event.Id);
        }
    }
}
