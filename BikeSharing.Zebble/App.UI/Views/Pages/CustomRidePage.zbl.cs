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
    using Zebble.Services;
    using UI._Custom;

    partial class CustomRidePage
    {
        public override async Task OnInitializing()
        {
            await base.OnInitializing();
            await InitializeComponents();
            MapView.ZoomLevel = 10;
            await MapView.Add(new Map.Annotation
            {
                Title = "Station",
                SubTitle = "Station 1",
                Location = new Zebble.Services.GeoLocation(GlobalSettings.EventLatitude, GlobalSettings.EventLongitude)
            });

            PathSelector.Y.Set(10);
        }
    }
}