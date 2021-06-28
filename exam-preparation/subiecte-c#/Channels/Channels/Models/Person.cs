using System;
using System.Collections.Generic;

namespace Channels.Models
{
    public partial class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public string Gender { get; set; }
    }
}
