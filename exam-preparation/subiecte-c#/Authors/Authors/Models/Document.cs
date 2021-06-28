using System;
using System.Collections.Generic;

namespace Authors.Models
{
    public partial class Document
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Contents { get; set; }
    }
}
