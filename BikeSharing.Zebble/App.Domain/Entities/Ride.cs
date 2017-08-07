using System;

namespace Domain
{
    public class Ride
    {
        bool _isSelected;

        public int Id { get; set; }

        public int? EventId { get; set; }

        public int BikeId { get; set; }

        public RideType RideType { get; set; }

        public string Name { get; set; }

        public DateTime Start { get; set; }

        public string StartString { get { return Start.ToString("dddd, MMMM dd"); } }

        public DateTime Stop { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public Station FromStation { get; set; }

        public Station ToStation { get; set; }

        public int Distance { get; set; }

        public int Duration { get; set; }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                //   RaisePropertyChanged(() => IsSelected);
            }
        }
    }
}