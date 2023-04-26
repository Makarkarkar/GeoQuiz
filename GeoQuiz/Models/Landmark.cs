using System;
using System.Collections.Generic;

namespace GeoQuiz
{
    public partial class Landmark
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public string? ImagePath { get; set; }
    }
}
