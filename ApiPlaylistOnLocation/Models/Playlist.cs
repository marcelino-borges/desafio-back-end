using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Playlist
    {
        public List<Track> Tracks { get; set; }

        public Playlist(List<Track> tracks)
        {
            Tracks = tracks;
        }

        public static bool IsPlaylistValid(Playlist playlist)
        {
            return playlist != null && playlist.Tracks != null && playlist.Tracks.Count > 0;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Playlist)) return false;
            if (!(obj as Playlist).Tracks.Equals(Tracks)) return false;
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"Playlist {{ {Tracks} }}";
        }
    }
}
