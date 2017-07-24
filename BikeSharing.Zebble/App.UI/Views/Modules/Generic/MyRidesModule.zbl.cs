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
    using UI._Custom;

    partial class MyRidesModule
    {
        public List<Ride> Items;
        public override async Task OnInitializing()
        {
          
            //var _ridesService = new RidesService();
            //var ridesResult = await _ridesService.GetUserRides();
            // Items = ridesResult.ToList();
            UriBuilder builder = new UriBuilder(GlobalSettings.RidesEndpoint);
            builder.Path = $"api/rides/user/1";

            string uri = builder.ToString();

    

            Items = await Api.Get<List<Ride>>(uri, cacheChoice: ApiResponseCache.PreferThenUpdate, refresher: Refresh);


            await base.OnInitializing();
            await InitializeComponents();

        }

        Task Refresh(List<Ride> items) => WhenShown(() => List.UpdateSource(Items = items));


        partial class Row
        {
            public override async Task OnInitializing()
            {
                await base.OnInitializing();
                await InitializeComponents();
               // this.Width.Set(300);
            }

            public Task RowTapped()
            {
               

                return Task.CompletedTask;
            }
        }
    }
}