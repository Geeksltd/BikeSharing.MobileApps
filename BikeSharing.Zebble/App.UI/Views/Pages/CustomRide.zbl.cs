namespace UI.Pages
{
    using Domain;
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using UI;
    using Zebble;
    using static Domain.Services.Api;

    partial class CustomRide
    {
        public ObservableCollection<CustomPin> CustomPins;

        public override async Task OnInitializing()
        {
            await base.OnInitializing();
            await InitializeComponents();
            MapView.ZoomLevel = 11;
            RouteSelector.Y.Set(10);
            RouteSelected.Y.Set(Root.ActualHeight - 230);
            FromItemPicker.SelectionChanged.Handle(FSelectionChanged);
            ToItemPicker.SelectionChanged.Handle(TSelectionChanged);
            if (await RidesService.GetNearestStations() != null)
            {
                //await MapView.Add(new Map.Annotation
                //{
                //    Title = (await new RidesService().GetNearestStations()).FirstOrDefault().Name,
                //    Location = new Zebble.Services.GeoLocation((await new RidesService().GetNearestStations()).FirstOrDefault().Latitude, (await new RidesService().GetNearestStations()).FirstOrDefault().Longitude)
                //});
                InitializePinsFromStations(await RidesService.GetNearestStations());
                FromItemPicker.DataSource = CustomPins.ToList();
                ToItemPicker.DataSource = CustomPins.ToList();
            }
        }

        private async Task FSelectionChanged()
        {
            var selected = (CustomPin)FromItemPicker.SelectedValue;

            var station = await RidesService.GetStation(selected.Id);
            RouteSelected.Visible = true;
            FromPS.Visible = true;
            FromText.Text = selected.Label;
            FromPSText.Text = $"Empty bike docks {station.EmptyDocks} Avilable bikes {station.Occupied}";
        }

        private async Task TSelectionChanged()
        {
            var selected = (CustomPin)ToItemPicker.SelectedValue;
            var station = await RidesService.GetStation(selected.Id);
            RouteSelected.Visible = true;
            ToPS.Visible = true;
            ToText.Text = selected.Label;
            ToPSText.Text = $"Empty bike docks {station.EmptyDocks} Avilable bikes {station.Occupied}";
        }

        public async Task GoClicked()
        {
            var fromStation = FromItemPicker.SelectedValue;
            var toStation = ToItemPicker.SelectedValue;

            await Nav.Go<BookingPage>(new { from = fromStation, to = toStation });
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