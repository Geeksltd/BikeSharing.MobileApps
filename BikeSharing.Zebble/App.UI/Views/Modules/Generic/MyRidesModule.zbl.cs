namespace UI.Modules
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Zebble;
    using Zebble.Framework;
    using Domain;
    using Domain.Services;

    partial class MyRidesModule
    {
        public List<Ride> Items;
        public override async Task OnInitializing()
        {
            await base.OnInitializing();
            await InitializeComponents();

            var _ridesService = new RidesService();
            var ridesResult = await _ridesService.GetUserRides();
           // Items = ridesResult.ToList();
        }


    }
}