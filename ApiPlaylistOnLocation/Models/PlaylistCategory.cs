using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class PlaylistCategory
    {
        public string Value { get; set; }
        public static readonly string party = "party";
        public static readonly string pop = "pop";
        public static readonly string rock = "rock";
        public static readonly string classic = "classical";

        public PlaylistCategory(string value)
        {
            Value = value;
        }
    }
}
