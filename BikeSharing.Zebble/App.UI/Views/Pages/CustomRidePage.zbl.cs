namespace UI.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using Domain;
    using Domain.Services;
    using UI;
    using Zebble;
    using Zebble.Plugin;

    partial class CustomRidePage
    {
        ObservableCollection<CustomPin> CustomPins;

        public override async Task OnInitializing()
        {
            await base.OnInitializing();
            await InitializeComponents();
            MapView.ZoomLevel = 11;
            RouteSelector.Y.Set(10);
            RouteSelected.Y.Set(Root.ActualHeight - 230);
            FromItemPicker.SelectionChanged.Handle(FSelectionChanged);
            ToItemPicker.SelectionChanged.Handle(TSelectionChanged);
            if (await new RidesService().GetNearestStations() != null)
            {
                await MapView.Add(new Map.Annotation
                {
                    Title = (await new RidesService().GetNearestStations()).FirstOrDefault().Name,
                    Location = new Zebble.Services.GeoLocation((await new RidesService().GetNearestStations()).FirstOrDefault().Latitude, (await new RidesService().GetNearestStations()).FirstOrDefault().Longitude)
                });
                InitializePinsFromStations(await new RidesService().GetNearestStations());
                FromItemPicker.DataSource = CustomPins.ToList();
                ToItemPicker.DataSource = CustomPins.ToList();
            }
        }

        async Task FSelectionChanged()
        {
            var selected = (CustomPin)FromItemPicker.SelectedValue;
            var _ridesService = new RidesService();
            var _stations = await _ridesService.GetStation(selected.Id);
            RouteSelected.Visible = true;
            FromPS.Visible = true;
            fromText.Text = selected.Label;
            FromPSText.Text = string.Format("Empty bike docks {0} Avilable bikes {1}", _stations.EmptyDocks, _stations.Occupied);
        }

        async Task TSelectionChanged()
        {
            var selected = (CustomPin)ToItemPicker.SelectedValue;
            var _ridesService = new RidesService();
            var _stations = await _ridesService.GetStation(selected.Id);
            RouteSelected.Visible = true;
            ToPS.Visible = true;
            toText.Text = selected.Label;
            FromPSText.Text = string.Format("Empty bike docks {0} Avilable bikes {1}", _stations.EmptyDocks, _stations.Occupied);
        }

        public async Task GoClicked()
        {
            var fromStation = FromItemPicker.SelectedValue;
            var toStation = ToItemPicker.SelectedValue;

            await Nav.Forward<BookingPage>(new { from = fromStation, to = toStation });
        }


        void InitializePinsFromStations(IEnumerable<Station> allStations)
        {
            if (allStations != null)
            {
                var tempStations = new ObservableCollection<CustomPin>();

                int counter = 1;
                foreach (var station in allStations)
                {
                    tempStations.Add(new CustomPin
                    {
                        Id = counter,
                        PinIcon = "pushpin",
                        Label = station.Name,
                        Address = string.Format("{0}, {1}", station.Latitude, station.Longitude),
                        Position = new Domain.GeoLocation(station.Latitude, station.Longitude)
                    });

                    counter++;
                }

                CustomPins = new ObservableCollection<CustomPin>(tempStations);
            }
        }
    }
}