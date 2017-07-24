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
    using UI._Custom;
    using Zebble.Plugin;

    partial class MyRidesPage
    {
        public override async Task OnInitializing()
        {
            await base.OnInitializing();
            await InitializeComponents();

            await MapView.Add(new Map.Annotation
            {
                Title = GlobalSettings.City,
                Location = new Zebble.Services.GeoLocation(GlobalSettings.EventLatitude, GlobalSettings.EventLongitude)
            });

            txtDate.Text = DateTime.Now.ToString("dddd, MMMM dd");
            txtCity.Text = _Custom.GlobalSettings.City;
        }
    }
}