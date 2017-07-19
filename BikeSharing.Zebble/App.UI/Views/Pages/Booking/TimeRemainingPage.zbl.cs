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

    partial class TimeRemainingPage
    {
        Timer timer;
        TimeSpan timeSpanCounter = new TimeSpan(0, 3, 1);


        public override async Task OnInitializing()
        {
            await base.OnInitializing();
            await InitializeComponents();

            var book = Nav.Param<Booking>("Booking");
            DateText.Text = book.DueDate.ToString("dddd, MMMM dd");
            CityText.Text = _Custom.GlobalSettings.City;
            fromText.Text = book.FromStation.Name;
            toText.Text = book.ToStation.Name;
            BookIdText.Text = string.Format("YOUR BIKE NUMBER IS {0}", book.BikeId.ToString());
            timer = new Timer(CounterFunc, null, 1, 1000);
        }


        private void CounterFunc(object state)
        {
            TimeSpan tp = new TimeSpan(0, 0, 1);
            timeSpanCounter= timeSpanCounter.Subtract(tp);
          
            if (timeSpanCounter.TotalSeconds > 0)
            {
                txtTimer.Text = timeSpanCounter.ToStringOrEmpty();
            } 
            else
                timer.Dispose();

        }
    }
}