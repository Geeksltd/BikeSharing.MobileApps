namespace UI.Pages
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Entities;
    using Domain.Services;
    using Zebble;
    using static Domain.Services.Api;

    partial class TimeRemaining
    {
        Timer timerControl;
        TimeSpan timeSpanCounter = new TimeSpan(0, 3, 1);
        Booking bookRequest;



        public override async Task OnInitializing()
        {
            bookRequest = Nav.Param<Booking>("Booking");

            if (bookRequest == null)
            {
                var userRides = await RidesService.GetUserRides();
                if (userRides != null)
                {
                    var ride = userRides.Where(rec => rec.Start > LocalTime.Now).OrderBy(rec => rec.Start).FirstOrDefault();
                    if (ride != null)
                    {
                        bookRequest = new Booking
                        {
                            BikeId = ride.BikeId,
                            DueDate = ride.Start.AddMinutes(ride.Duration),
                            EventId = ride.EventId,
                            FromStation = ride.FromStation,
                            Id = ride.Id,
                            RegistrationDate = ride.Start,
                            RideType = ride.RideType,
                            ToStation = ride.ToStation
                        };
                    }
                }
            }
            if (bookRequest == null)
            {
                await Alert.Show("Alert", "There is no available ride");
                return;
            }
            await base.OnInitializing();
            await InitializeComponents();
            timerControl = new Timer(CounterFunc, null, 1, 1000);
        }


        void CounterFunc(object state)
        {
            var tp = new TimeSpan(0, 0, 1);
            timeSpanCounter = timeSpanCounter.Subtract(tp);

            if (timeSpanCounter.TotalSeconds >= 0)
            {
                TimerText.Text = timeSpanCounter.ToString("m\\:ss");
            }
            else
                timerControl.Dispose();
        }
    }
}