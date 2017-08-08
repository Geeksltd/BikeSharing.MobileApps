namespace UI.Pages
{
    using Domain.Entities;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Zebble;

    partial class BookingDetail
    {
        Timer timerControl;
        Booking bookRequest;
        bool isFirst = true;
        DateTime dueDate = LocalTime.Now;
        public override async Task OnInitializing()
        {
            if (Nav.Param<bool>("ShowThanks"))
            {
                bookRequest = Nav.Param<Booking>("Booking");
                timerControl = new Timer(NavToTimeRemaining, null, 1, 4000);
            }
            else
            {
                await Alert.Show("Alert", "Booking was not success");
                await Nav.Go<Home>();
            }
            await base.OnInitializing();
            await InitializeComponents();
        }

        void NavToTimeRemaining(object state)
        {
            if (!isFirst)
            {
                timerControl.Dispose();
                isFirst = false;
                Nav.Go<TimeRemaining>(new { Booking = bookRequest });
            }
            else
                isFirst = false;
        }
    }
}