namespace UI.Pages
{
    using Domain;
    using System.Threading.Tasks;
    using Zebble.Plugin;
    using static Domain.Services.Api;
    partial class MyRides
    {
        public Ride[] Items;
        public override async Task OnInitializing()
        {
            Items = await RidesService.GetUserRides(refresher: Refresh);

            await base.OnInitializing();
            await InitializeComponents();

            if (Items != null && Items.Length > 0)
            {
                var Item = Items[0];
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
            //    MapView.Center = new Zebble.Services.GeoLocation(((Item.ToStation.Latitude + Item.FromStation.Latitude) / 2), ((Item.ToStation.Longitude + Item.FromStation.Longitude) / 2));
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
            public MyRides Module => FindParent<MyRides>();

            public override async Task OnInitializing()
            {
                await base.OnInitializing();
                await InitializeComponents();
                RideInfoModule.From = new CustomPin { Label = Item.From };
                RideInfoModule.To = new CustomPin { Label = Item.To };
            }


            public async Task RowTapped()
            {
                Module.MapView.ZoomLevel--;
              //  Module.MapView.Annotations.Clear();

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
            //    Module.MapView.Center = new Zebble.Services.GeoLocation(((Item.ToStation.Latitude + Item.FromStation.Latitude) / 2), ((Item.ToStation.Longitude + Item.FromStation.Longitude) / 2));

                Module.BookModule.Visible = true;
                Module.BookModule.From = new CustomPin { Label = Item.From };
                Module.BookModule.To = new CustomPin { Label = Item.To };
                await Module.BookModule.ChangeValues();
            }
        }
    }
}