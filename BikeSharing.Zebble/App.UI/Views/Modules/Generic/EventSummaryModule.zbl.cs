namespace UI.Modules
{
    using Domain;
    using Domain.Entities;
    using Domain.Services;
    using System.Threading.Tasks;
    using UI.Pages;
    using Zebble;

    partial class EventSummaryModule
    {
        public Event Item;
        Station FromStation, ToStation;

        public override async Task OnInitializing()
        {
            var Id = Nav.Param<int>("Id");
            Item = await new EventsService().GetEventById(Id);

            var toGeoLocation = new Domain.GeoLocation(Item.Venue.Latitude, Item.Venue.Longitude);
            FromStation = await new RidesService().GetInfoForNearestStation();
            ToStation = await new RidesService().GetInfoForNearestStationTo(toGeoLocation);

            await base.OnInitializing();
            await InitializeComponents();
            eventModule.Item = Item;
        }

        async Task BookBikeButtonTapped()
        {
            Booking booking = await new RidesService().RequestBikeBooking(FromStation, ToStation, Item);
            await Nav.Go<BookingDetailPage>(new { ShowThanks = true, Booking = booking });
        }
    }
}