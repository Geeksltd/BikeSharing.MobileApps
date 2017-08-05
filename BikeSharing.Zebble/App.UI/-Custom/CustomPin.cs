using Domain;

namespace UI
{
    public class CustomPin
    {
        public enum AnnotationType
        {
            Normal,
            From,
            To
        };

        public int Id { get; set; }

        public string Label { get; set; }

        public string Address { get; set; }

        public string PinIcon { get; set; }

        public GeoLocation Position { get; set; }

        public AnnotationType Type { get; set; }

        public override string ToString()
        {
            return Label;
        }
    }
}