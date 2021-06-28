using System;
using System.Collections.Generic;

namespace Authors.Models
{
    public partial class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? Duration { get; set; }
    }
}
