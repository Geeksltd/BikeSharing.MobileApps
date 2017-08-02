namespace UI.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Zebble;
     
    using Domain;
    using System.Threading;
    using Domain.Entities;
    using Domain.Services;

    partial class TimeRemainingPage
    {
        Timer timer;
        TimeSpan timeSpanCounter = new TimeSpan(0, 3, 1);
        Booking book;

        public override async Task OnInitializing()
        {
            book = Nav.Param<Booking>("Booking");

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
            if (book == null)
            {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed. Consider applying the 'await' operator to the result of the call.
                Alert.Show("Alert", "There is no available ride");
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed. Consider applying the 'await' operator to the result of the call.
                return;
            }
            await base.OnInitializing();
            await InitializeComponents();
            timer = new Timer(CounterFunc, null, 1, 1000);
        }


        private void CounterFunc(object state)
        {
            TimeSpan tp = new TimeSpan(0, 0, 1);
            timeSpanCounter= timeSpanCounter.Subtract(tp);
          
            if (timeSpanCounter.TotalSeconds >= 0)
            {
                timerText.Text = timeSpanCounter.ToString("m\\:ss");
            } 
            else
                timer.Dispose();

        }
    }
}