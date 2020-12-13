namespace API.Models
{
    public class Track
    {

        public string Name { get; }

        public Track(string name)
        {
            Name = name;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Track)) return false;
            if (!(obj as Track).Name.Equals(Name)) return false;
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"Track {{ Name: {Name} }}";
        }
    }
}
