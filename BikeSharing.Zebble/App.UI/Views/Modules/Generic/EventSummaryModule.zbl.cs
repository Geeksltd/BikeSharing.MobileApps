namespace UI.Modules
{
    using Domain;
    using System.Threading.Tasks;
    using UI.Pages;
    using Zebble;
    using static Domain.Services.Api;

    partial class EventSummaryModule
    {
        Event item;
        Station fromStation, toStation;

        public Event Item { get => item; set => item = value; }

        public override async Task OnInitializing()
        {
            Item = await EventsService.GetEventById(Nav.Param<int>("Id"));
            if (Item != null)
            {
                var toGeoLocation = new Domain.GeoLocation(Item.Venue.Latitude, Item.Venue.Longitude);
                fromStation = await RidesService.GetInfoForNearestStation();
                toStation = await RidesService.GetInfoForNearestStationTo(toGeoLocation);
            }
            await base.OnInitializing();
            await InitializeComponents();
            EventModule.Item = Item;
            BookStationsModule.From = new CustomPin { Label = fromStation.Name };
            BookStationsModule.To = new CustomPin { Label = toStation.Name };
        }

        async Task BookBikeButtonTapped()
        {
            var booking = await RidesService.RequestBikeBooking(fromStation, toStation, Item);
            await Nav.Go<BookingDetail>(new { ShowThanks = true, Booking = booking });
        }
    }
}