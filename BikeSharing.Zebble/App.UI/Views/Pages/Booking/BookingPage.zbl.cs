namespace UI.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Zebble;   
    using Domain;
    using UI;
    using Domain.Entities;
    using Domain.Services;
    using Zebble.Plugin;

    partial class BookingPage
    {
        CustomPin From;
        CustomPin To;
        bool IsBusy = false;
        public override async Task OnInitializing()
        {
            await base.OnInitializing();
            await InitializeComponents();
            MainStack.Y.Set((float)0);

             From = Nav.Param<CustomPin>("from");
             To = Nav.Param<CustomPin>("to");

            fromText.Text = From.Label;
            toText.Text = To.Label;
            DateText.Text = DateTime.Now.ToString("dddd, MMMM dd");
            CityText.Text = GlobalSettings.City;
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
            if (!IsBusy)
            {
                IsBusy = true;
                try
                {
                    var _ridesService = new RidesService();
                    var FromStation = await _ridesService.GetStation(From.Id);
                    var ToStation = await _ridesService.GetStation(To.Id);
                    Booking booking = await _ridesService.RequestBikeBooking(FromStation, ToStation);
                   
                    await Nav.Forward<ThanksBookingPage>(new { ShowThanks = true, Booking = booking });
                }
                catch (Exception ex)
                {
                    //  await DialogService.ShowAlertAsync("We are sorry, there are no bikes in origin station", "No bike available", "Ok");
                }
                IsBusy = false;
            }
        }
    }
}