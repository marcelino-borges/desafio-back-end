namespace API.Models
{
    public class Coordinate
    {
        public string Latitude { get; }
        public string Longitude { get; }

        public Coordinate(string latitude, string longitude)
        {
            Latitude = latitude.Replace(",", ".");
            Longitude = longitude.Replace(",", ".");
        }

        public static bool IsValid(Coordinate coordinate)
        {
            return coordinate != null &&
                !string.IsNullOrEmpty(coordinate.Latitude) &&
                !string.IsNullOrEmpty(coordinate.Longitude);
        }

        public override bool Equals(object obj)
        {
            if(!(obj is Coordinate)) return false;
            Coordinate coord = (obj as Coordinate);
            if (coord.Latitude != Latitude || coord.Longitude != Longitude)
                return false;
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"Coordinate {{ Latitude: {Latitude}, Longitude: {Longitude} }}";
        }
    }
}
