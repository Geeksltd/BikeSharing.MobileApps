using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Booking
    {
        public int Id { get; set; }

        public int BikeId { get; set; }

        public int? EventId { get; set; }

        public RideType RideType { get; set; }

        public Station FromStation { get; set; }

        public Station ToStation { get; set; }

        public DateTime RegistrationDate { get; set; }

        public DateTime DueDate { get; set; }

        public string DueDateString { get { return DueDate.ToString("dddd, MMMM dd"); } }
    }
}
