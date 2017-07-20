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

    partial class ThanksBookingPage
    {
        Timer timer ;
        Booking book ;
        bool isFirst = true;
        DateTime dueDate = DateTime.Now;
        public override async Task OnInitializing()
        {
            await base.OnInitializing();
            await InitializeComponents();

            var ShowThanks = Nav.Param<bool>("ShowThanks");
            if (ShowThanks)
            {
                book = Nav.Param<Booking>("Booking"); 
                

                txtDate.Text =book.DueDate.ToString("dddd, MMMM dd");
                txtCity.Text = _Custom.GlobalSettings.City;

                txtBookId.Text =  book.BikeId.ToString();

                timer= new Timer(NavToTimeRemaining,null, 1, 4000);          
            }
        }

        private void NavToTimeRemaining(object state)
        {
            if (!isFirst)
            {
                timer.Dispose();
                isFirst = false;
                Nav.Forward<TimeRemainingPage>(new { Booking = book });
            }
            else
                isFirst = false;
        }
    }
}