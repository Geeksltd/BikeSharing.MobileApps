namespace UI.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Zebble;
    using Zebble.Framework;
    using Domain;
    using System.Threading;
    using Domain.Entities;
    using Domain.Services;

    partial class TimeRemainingPage
    {
        Timer timer;
        TimeSpan timeSpanCounter = new TimeSpan(0, 3, 1);


        public override async Task OnInitializing()
        {
            await base.OnInitializing();
            await InitializeComponents();

            var book = Nav.Param<Booking>("Booking");

            if (book == null)
            {
                var _ridesService = new RidesService();
                var _rides = await _ridesService.GetUserRides();
                if (_rides != null)
                {
                    var ride = _rides.Where(rec => rec.Start > DateTime.Now).OrderBy(rec => rec.Start).FirstOrDefault();
                    if(ride != null)
                    {
                        book = new Booking();
                        book.BikeId = ride.BikeId;
                        book.DueDate = ride.Start.AddMinutes(ride.Duration);
                        book.EventId = ride.EventId;
                        book.FromStation = ride.FromStation;
                        book.Id = ride.Id;
                        book.RegistrationDate = ride.Start;
                        book.RideType = ride.RideType;
                        book.ToStation = ride.ToStation;                       
                    }
                }
            }
            if (book != null)
            {
                txtDate.Text = book.DueDate.ToString("dddd, MMMM dd");
                txtCity.Text = GlobalSettings.City;
                txtFrom.Text = book.FromStation.Name;
                txtTo.Text = book.ToStation.Name;
                txtBookId.Text = book.BikeId.ToString();
                timer = new Timer(CounterFunc, null, 1, 1000);
            }
            else
            {
                Alert.Show("Alert", "There is no available ride");
                return;
            }
        }


        private void CounterFunc(object state)
        {
            TimeSpan tp = new TimeSpan(0, 0, 1);
            timeSpanCounter= timeSpanCounter.Subtract(tp);
          
            if (timeSpanCounter.TotalSeconds >= 0)
            {
                txtTimer.Text = timeSpanCounter.ToString("m\\:ss");
            } 
            else
                timer.Dispose();

        }
    }
}