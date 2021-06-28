using System;
using System.Collections.Generic;

namespace Channels.Models
{
    public partial class Channel
    {
        public int Id { get; set; }
        public int? Ownerid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Subscribers { get; set; }
    }
}
