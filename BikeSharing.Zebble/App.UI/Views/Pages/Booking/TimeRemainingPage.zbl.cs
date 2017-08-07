namespace UI.Pages
{
    using Domain.Entities;
    using Domain.Services;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Zebble;
    using static Domain.Services.Api;

    partial class TimeRemainingPage
    {
        Timer timer;
        TimeSpan TimeSpanCounter = new TimeSpan(0, 3, 1);
        Booking Book;



        public override async Task OnInitializing()
        {
            Book = Nav.Param<Booking>("Booking");

            if (Book == null)
            {
                if (await RidesService.GetUserRides() != null)
                {
                    var ride = (await RidesService.GetUserRides()).Where(rec => rec.Start > LocalTime.Now).OrderBy(rec => rec.Start).FirstOrDefault();
                    if (ride != null)
                    {
                        Book = new Booking()
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
            if (Book == null)
            {
                await Alert.Show("Alert", "There is no available ride");
                return;
            }
            await base.OnInitializing();
            await InitializeComponents();
            timer = new Timer(CounterFunc, null, 1, 1000);
        }


        private void CounterFunc(object state)
        {
            var tp = new TimeSpan(0, 0, 1);
            TimeSpanCounter = TimeSpanCounter.Subtract(tp);

            if (TimeSpanCounter.TotalSeconds >= 0)
            {
                timerText.Text = TimeSpanCounter.ToString("m\\:ss");
            }
            else
                timer.Dispose();

        }
    }
}