namespace UI.Pages
{
    using Domain;
    using System.Threading.Tasks;
    using static Domain.Services.Api;
    partial class MyRides
    {
        public Ride[] Items;
        public override async Task OnInitializing()
        {
            Items = await RidesService.GetUserRides(refresher: Refresh);

            await base.OnInitializing();
            await InitializeComponents();

            if (Items != null)
            {
                //var Item = Items.FirstOrDefault();
                //await MapView.Add(new Map.Annotation
                //{
                //    Title = Item.From,
                //    Location = new Zebble.Services.GeoLocation(Item.FromStation.Latitude, Item.FromStation.Longitude)
                //});
                //await MapView.Add(new Map.Annotation
                //{
                //    Title = Item.To,
                //    Location = new Zebble.Services.GeoLocation(Item.ToStation.Latitude, Item.ToStation.Longitude)
                //});
                //MapView.Center = new Zebble.Services.GeoLocation(((Item.ToStation.Latitude + Item.FromStation.Latitude) / 2), ((Item.ToStation.Longitude + Item.FromStation.Longitude) / 2));
            }
            //else
            //    await MapView.Add(new Map.Annotation
            //    {
            //        Title = GlobalSettings.City,
            //        Location = new Zebble.Services.GeoLocation(GlobalSettings.EventLatitude, GlobalSettings.EventLongitude)
            //    });

        }

        Task Refresh(Ride[] items) => WhenShown(() => List.UpdateSource(Items = items));

        partial class Row
        {
            public MyRides Module => FindParent<MyRides>();

            public override async Task OnInitializing()
            {
                await base.OnInitializing();
                await InitializeComponents();
            }

            public async Task RowTapped()
            {
                // Module.MapView.ZoomLevel--;
                //Module.MapView.Annotations.Clear();

                //await Module.MapView.Add(new Map.Annotation
                //{
                //    Title = Item.From,
                //    Location = new Zebble.Services.GeoLocation(Item.FromStation.Latitude, Item.FromStation.Longitude)
                //});
                //await Module.MapView.Add(new Map.Annotation
                //{
                //    Title = Item.To,
                //    Location = new Zebble.Services.GeoLocation(Item.ToStation.Latitude, Item.ToStation.Longitude)
                //});

                await Task.Delay(500);
                // Module.MapView.ZoomLevel++;
                // Module.MapView.Center = new Zebble.Services.GeoLocation(((Item.ToStation.Latitude + Item.FromStation.Latitude) / 2), ((Item.ToStation.Longitude + Item.FromStation.Longitude) / 2));

                Module.FromSNSelectedRowTextView.Text = Item.From;
                Module.ToSNSelectedRowTextView.Text = Item.To;
                Module.DateSelectedRowTextView.Text = Item.StartString;
                Module.SelectedStack.Visible = true;
            }
        }

    }
}