namespace UI.Modules
{
    using Domain;
    using System.Threading.Tasks;
    using UI.Pages;
    using Zebble;
    using static Domain.Services.Api;

    partial class EventSummaryModule
    {
        private Event item;
        Station FromStation, ToStation;

        public Event Item { get => item; set => item = value; }

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
            EventModule.Item = Item;
        }

        async Task BookBikeButtonTapped()
        {
            var booking = await RidesService.RequestBikeBooking(FromStation, ToStation, Item);
            await Nav.Go<BookingDetail>(new { ShowThanks = true, Booking = booking });
        }
    }
}