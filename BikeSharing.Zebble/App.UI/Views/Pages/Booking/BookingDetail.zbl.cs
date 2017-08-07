namespace UI.Pages
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Entities;
    using Zebble;

    partial class BookingDetailPage
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
                await Nav.Go<HomePage>();
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
                Nav.Forward<TimeRemainingPage>(new { Booking = bookRequest });
            }
            else
                isFirst = false;
        }
    }
}