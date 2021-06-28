using System;
using System.Collections.Generic;

namespace Authors.Models
{
    public partial class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DocumentList { get; set; }
        public string MovieList { get; set; }
    }
}
