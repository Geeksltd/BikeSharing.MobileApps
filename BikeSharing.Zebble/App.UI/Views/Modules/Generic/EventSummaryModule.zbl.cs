namespace UI.Modules
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Zebble;
    using Domain;
    using Domain.Services;
    using System.Net;
    using System.Net.Http;
    using Domain.Entities;
    using UI.Pages;

    partial class EventSummaryModule
    {
        public Event Item;
        Station FromStation, ToStation;
        EventsService _eventsService;
        RidesService _ridesService ;
        public override async Task OnInitializing()
        {
            try
            {
                
                var Id = Nav.Param<int>("Id");
                _eventsService = new EventsService();
                Item = await _eventsService.GetEventById(Id);

                var toGeoLocation = new Domain.GeoLocation(Item.Venue.Latitude, Item.Venue.Longitude);
                _ridesService = new RidesService();
                FromStation = await _ridesService.GetInfoForNearestStation();
                ToStation = await _ridesService.GetInfoForNearestStationTo(toGeoLocation);
            }
            catch (Exception ex) when (ex is WebException)
            {
                await Alert.Show("Error", "Communication error");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data in: {ex}");
            }
            await base.OnInitializing();
            await InitializeComponents();
            eventModule.Item = Item;
        }


        async Task BuyTicketButtonTapped()
        {

        }


        async Task BookBikeButtonTapped()
        {
            try
            {
                Booking booking = await _ridesService.RequestBikeBooking(FromStation, ToStation, Item);
                await Nav.Forward<BookingDetailPage>(new { ShowThanks = true, Booking = booking });

            }
            //catch (NoAvailableBikesException)
            //{
            //    await DialogService.ShowAlertAsync("We are sorry, there are no bikes in origin station", "No bike available", "Ok");
            //}
            catch (Exception ex) when (ex is WebException )
            {
                await Alert.Show("Error","Communication error");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data in: {ex}");
            }
        }


    }
}