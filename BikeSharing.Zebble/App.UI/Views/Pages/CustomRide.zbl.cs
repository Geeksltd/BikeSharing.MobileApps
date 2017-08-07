namespace UI.Pages
{
    using Domain;
    using Domain.Services;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using UI;
    using Zebble;
    using Zebble.Plugin;
    using static Domain.Services.Api;

    partial class CustomRidePage
    {
        public ObservableCollection<CustomPin> CustomPins;

        public override async Task OnInitializing()
        {
            await base.OnInitializing();
            await InitializeComponents();
            // MapView.ZoomLevel = 11;
            routeSelector.Y.Set(10);
            routeSelected.Y.Set(Root.ActualHeight - 230);
            fromItemPicker.SelectionChanged.Handle(FSelectionChanged);
            toItemPicker.SelectionChanged.Handle(TSelectionChanged);
            if (await RidesService.GetNearestStations() != null)
            {
                //await MapView.Add(new Map.Annotation
                //{
                //    Title = (await new RidesService().GetNearestStations()).FirstOrDefault().Name,
                //    Location = new Zebble.Services.GeoLocation((await new RidesService().GetNearestStations()).FirstOrDefault().Latitude, (await new RidesService().GetNearestStations()).FirstOrDefault().Longitude)
                //});
                InitializePinsFromStations(await RidesService.GetNearestStations());
                fromItemPicker.DataSource = CustomPins.ToList();
                toItemPicker.DataSource = CustomPins.ToList();
            }
        }

        private async Task FSelectionChanged()
        {
            var selected = (CustomPin)fromItemPicker.SelectedValue;
            var station = await RidesService.GetStation(selected.Id);
            routeSelected.Visible = true;
            fromPS.Visible = true;
            fromText.Text = selected.Label;
            fromPSText.Text = $"Empty bike docks {station.EmptyDocks} Avilable bikes {station.Occupied}";
        }

        private async Task TSelectionChanged()
        {
            var selected = (CustomPin)toItemPicker.SelectedValue;
            var station = await RidesService.GetStation(selected.Id);
            routeSelected.Visible = true;
            toPS.Visible = true;
            toText.Text = selected.Label;
            toPSText.Text = $"Empty bike docks {station.EmptyDocks} Avilable bikes {station.Occupied}";
        }

        public async Task GoClicked()
        {
            var fromStation = fromItemPicker.SelectedValue;
            var toStation = toItemPicker.SelectedValue;

            await Nav.Forward<BookingPage>(new { from = fromStation, to = toStation });
        }


        private void InitializePinsFromStations(Station[] allStations)
        {
            if (allStations == null)
                return;

            var tempStations = new ObservableCollection<CustomPin>();

            int counter = 1;
            foreach (var station in allStations)
            {
                tempStations.Add(new CustomPin
                {
                    Id = counter,
                    PinIcon = "pushpin",
                    Label = station.Name,
                    Address = $"{station.Latitude}, {station.Longitude}",
                    Position = new Domain.GeoLocation(station.Latitude, station.Longitude)
                });
                counter++;
            }
            CustomPins = new ObservableCollection<CustomPin>(tempStations);
        }
    }
}