namespace UI.Pages
{
    using System;
    using System.Threading.Tasks;
    using UI;
    using UI.Modules;
    using Zebble;
    using Zebble.Plugin;
    using static Domain.Services.Api;

    partial class BookingPage
    {
        CustomPin From;
        CustomPin To;

        public override async Task OnInitializing()
        {
            From = Nav.Param<CustomPin>("from");
            To = Nav.Param<CustomPin>("to");

            await base.OnInitializing();
            await InitializeComponents();
            MainStack.Y.Set((float)0);
            

            await MapView.Add(new Map.Annotation
            {
                Title = From.Label,
                Location = new Zebble.Services.GeoLocation(From.Position.Latitude, From.Position.Longitude)
            });

            await MapView.Add(new Map.Annotation
            {
                Title = To.Label,
                Location = new Zebble.Services.GeoLocation(To.Position.Latitude, To.Position.Longitude)
            });
            MapView.ZoomLevel = 14;
        }

        public async Task BookClicked()
        {
            var fromStation = await RidesService.GetStation(From.Id);
            var toStation = await RidesService.GetStation(To.Id);
            var booking = await RidesService.RequestBikeBooking(fromStation, toStation);
            if (booking != null)
            {
                // FindParent<MainMenu>().UpcomingRideButton.Enabled = true;
                await Nav.Go<BookingDetail>(new { ShowThanks = true, Booking = booking });
            }
        }
    }
}