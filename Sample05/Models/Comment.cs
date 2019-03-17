using System;

namespace Sample05.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int ContentId { get; set; }
        public string Content { get; set; }
        public DateTime AddTime { get; set; }
    }
}