using System;
using System.Collections.Generic;

namespace Topics.Models
{
    public partial class Post
    {
        public int Id { get; set; }
        public string User { get; set; }
        public int? TopicId { get; set; }
        public string Text { get; set; }
        public int? Date { get; set; }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(User)}: {User}, {nameof(TopicId)}: {TopicId}, {nameof(Text)}: {Text}, {nameof(Date)}: {Date}";
        }
    }
}
