using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class WeatherInfo
    {
        public float Temp { get; set; }
        public TempUnit TempUnit { get; set; }
        public string LocationName { get; set; }
    }
}