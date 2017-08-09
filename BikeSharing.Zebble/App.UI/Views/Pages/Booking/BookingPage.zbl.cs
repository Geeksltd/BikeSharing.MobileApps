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
        CustomPin fromPin;
        CustomPin toPin;

        public override async Task OnInitializing()
        {
            fromPin = Nav.Param<CustomPin>("from");
            toPin = Nav.Param<CustomPin>("to");

            await base.OnInitializing();
            await InitializeComponents();
            MainStack.Y.Set((float)0);
            DateText.Text = LocalTime.Now.ToString("dddd, MMMM dd");

            await MapView.Add(new Map.Annotation
            {
                Title = fromPin.Label,
                Location = new Zebble.Services.GeoLocation(fromPin.Position.Latitude, fromPin.Position.Longitude)
            });

            await MapView.Add(new Map.Annotation
            {
                Title = toPin.Label,
                Location = new Zebble.Services.GeoLocation(toPin.Position.Latitude, toPin.Position.Longitude)
            });
            MapView.ZoomLevel = 14;
        }

        public async Task BookClicked()
        {
            var fromStation = await RidesService.GetStation(fromPin.Id);
            var toStation = await RidesService.GetStation(toPin.Id);
            var booking = await RidesService.RequestBikeBooking(fromStation, toStation);
            if (booking != null)
            {
                // FindParent<MainMenu>().UpcomingRideButton.Enabled = true;
                await Nav.Go<BookingDetail>(new { ShowThanks = true, Booking = booking });
            }
        }
    }
}