using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Ride 
    {
        private bool _isSelected;

        public int Id { get; set; }

        public int? EventId { get; set; }

        public RideType RideType { get; set; }

        public string Name { get; set; }

        public DateTime Start { get; set; }

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