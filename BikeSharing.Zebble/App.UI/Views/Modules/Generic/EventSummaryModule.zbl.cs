namespace UI.Modules
{
    using System.Threading.Tasks;
    using Domain;
    using Domain.Entities;
    using Domain.Services;
    using UI.Pages;
    using Zebble;
    using static Domain.Services.Api;

    partial class EventSummaryModule
    {
        public Event Item;
        Station FromStation, ToStation;

        public override async Task OnInitializing()
        {
            Item = await EventsService.GetEventById(Nav.Param<int>("Id"));
            if (Item != null)
            {
                var toGeoLocation = new Domain.GeoLocation(Item.Venue.Latitude, Item.Venue.Longitude);
                FromStation = await RidesService.GetInfoForNearestStation();
                ToStation = await RidesService.GetInfoForNearestStationTo(toGeoLocation);
            }
            await base.OnInitializing();
            await InitializeComponents();
            eventModule.Item = Item;
        }

        async Task BookBikeButtonTapped()
        {
            var booking = await RidesService.RequestBikeBooking(FromStation, ToStation, Item);
            await Nav.Go<BookingDetail>(new { ShowThanks = true, Booking = booking });
        }
    }
}