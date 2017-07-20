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
            txtDate.Text = book.DueDate.ToString("dddd, MMMM dd");
            txtCity.Text = _Custom.GlobalSettings.City;
            txtFrom.Text = book.FromStation.Name;
            txtTo.Text = book.ToStation.Name;
            txtBookId.Text =  book.BikeId.ToString();
            timer = new Timer(CounterFunc, null, 1, 1000);
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