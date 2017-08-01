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

    partial class BookingDetailPage
    {
        Timer timer ;
        Booking book ;
        bool isFirst = true;
        DateTime dueDate = DateTime.Now;
        public override async Task OnInitializing()
        {
            var ShowThanks = Nav.Param<bool>("ShowThanks");
            if (ShowThanks)
            {
                book = Nav.Param<Booking>("Booking");
                timer = new Timer(NavToTimeRemaining, null, 1, 4000);
            }
            else
            {
                Alert.Show("Alert", "Booking was not success");
                Nav.Go<HomePage>();
            }
            await base.OnInitializing();
            await InitializeComponents();
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