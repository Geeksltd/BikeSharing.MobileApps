namespace Domain
{
    public class WeatherInfo
    {
        public float Temp { get; set; }

        public TempUnit TempUnit { get; set; }

        public string TempUnitShort => TempUnit.ToString().Substring(0, 1);

        public string LocationName { get; set; }
    }
}