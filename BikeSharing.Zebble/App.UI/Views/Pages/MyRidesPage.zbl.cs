namespace UI.Pages
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Domain;
    using UI;
    using Zebble;
    using Zebble.Plugin;

    partial class MyRidesPage
    {
        public Ride[] Items;
        public override async Task OnInitializing()
        {
            UriBuilder builder = new UriBuilder(GlobalSettings.RidesEndpoint);
            builder.Path = $"api/rides/user/" + Settings.UserId.ToString();
            string uri = builder.ToString();
            Items = await BaseApi.Get<Ride[]>(uri, cacheChoice: ApiResponseCache.PreferThenUpdate, refresher: Refresh);

            await base.OnInitializing();
            await InitializeComponents();

            if (Items != null)
            {
                var Item = Items.FirstOrDefault();
                await MapView.Add(new Map.Annotation
                {
                    Title = Item.From,
                    Location = new Zebble.Services.GeoLocation(Item.FromStation.Latitude, Item.FromStation.Longitude)
                });
                await MapView.Add(new Map.Annotation
                {
                    Title = Item.To,
                    Location = new Zebble.Services.GeoLocation(Item.ToStation.Latitude, Item.ToStation.Longitude)
                });
                MapView.Center = new Zebble.Services.GeoLocation(((Item.ToStation.Latitude + Item.FromStation.Latitude) / 2), ((Item.ToStation.Longitude + Item.FromStation.Longitude) / 2));
            }
            else
                await MapView.Add(new Map.Annotation
                {
                    Title = GlobalSettings.City,
                    Location = new Zebble.Services.GeoLocation(GlobalSettings.EventLatitude, GlobalSettings.EventLongitude)
                });

        }

        Task Refresh(Ride[] items) => WhenShown(() => List.UpdateSource(Items = items));

        partial class Row
        {
            public MyRidesPage Module => FindParent<MyRidesPage>();

            public override async Task OnInitializing()
            {
                await base.OnInitializing();
                await InitializeComponents();
            }

            public async Task RowTapped()
            {
                Module.MapView.ZoomLevel--;
                Module.MapView.Annotations.Clear();

                await Module.MapView.Add(new Map.Annotation
                {
                    Title = Item.From,
                    Location = new Zebble.Services.GeoLocation(Item.FromStation.Latitude, Item.FromStation.Longitude)
                });
                await Module.MapView.Add(new Map.Annotation
                {
                    Title = Item.To,
                    Location = new Zebble.Services.GeoLocation(Item.ToStation.Latitude, Item.ToStation.Longitude)
                });

                await Task.Delay(500);
                Module.MapView.ZoomLevel++;
                Module.MapView.Center = new Zebble.Services.GeoLocation(((Item.ToStation.Latitude + Item.FromStation.Latitude) / 2), ((Item.ToStation.Longitude + Item.FromStation.Longitude) / 2));

                Module.fromSNSelectedRowTextView.Text = Item.From;
                Module.toSNSelectedRowTextView.Text = Item.To;
                Module.dateSelectedRowTextView.Text = Item.StartString;
                Module.selectedStack.Visible = true;
            }
        }

    }
}