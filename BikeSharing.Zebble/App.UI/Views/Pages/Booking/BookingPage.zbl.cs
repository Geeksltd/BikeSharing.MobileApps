namespace UI.Pages
{
    using Domain.Entities;
    using Domain.Services;
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
        bool IsBusy = false;
        public override async Task OnInitializing()
        {
            From = Nav.Param<CustomPin>("from");
            To = Nav.Param<CustomPin>("to");

            await base.OnInitializing();
            await InitializeComponents();
            MainStack.Y.Set((float)0);
            DateText.Text = DateTime.Now.ToString("dddd, MMMM dd");

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
            if (!IsBusy)
            {
                IsBusy = true;
                try
                {
                    var FromStation = await  RidesService.GetStation(From.Id);
                    var ToStation = await  RidesService.GetStation(To.Id);
                    Booking booking = await  RidesService.RequestBikeBooking(FromStation, ToStation);
                    // if (booking != null)
                    //    FindParent<MainMenu>().upcomingRideButton.Enabled = true;
                    await Nav.Forward<BookingDetailPage>(new { ShowThanks = true, Booking = booking });
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    await Alert.Show("No bike available", "We are sorry, there are no bikes in origin station");
                }
                IsBusy = false;
            }
        }
    }
}