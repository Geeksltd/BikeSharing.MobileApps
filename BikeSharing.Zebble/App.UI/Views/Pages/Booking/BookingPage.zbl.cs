namespace UI.Pages
{
    using System;
    using System.Threading.Tasks;
    using Domain.Entities;
    using Domain.Services;
    using UI;
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

            //await MapView.Add(new Map.Annotation
            //{
            //    Title = From.Label,
            //    Location = new Zebble.Services.GeoLocation(From.Position.Latitude, From.Position.Longitude)
            //});

            //await MapView.Add(new Map.Annotation
            //{
            //    Title = To.Label,
            //    Location = new Zebble.Services.GeoLocation(To.Position.Latitude, To.Position.Longitude)
            //});
            //MapView.ZoomLevel = 14;
        }



        public async Task BookClicked()
        {

            // try
            // {
            var fromStation = await RidesService.GetStation(fromPin.Id);
            var toStation = await RidesService.GetStation(toPin.Id);
            var booking = await RidesService.RequestBikeBooking(fromStation, toStation);
            // if (booking != null)
            //    FindParent<MainMenu>().upcomingRideButton.Enabled = true;
            await Nav.Go<BookingDetail>(new { ShowThanks = true, Booking = booking });
            //}
            //catch (Exception ex)
            //{
            //    Console.Write(ex.Message);
            //    await Alert.Show("No bike available", "We are sorry, there are no bikes in origin station");
            //}

        }
    }
}