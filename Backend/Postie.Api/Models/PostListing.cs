using System;

namespace Postie.Api.Models
{
    public class PostListing
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public bool Hidden { get; set; }

        public string Username { get; set; }

        public DateTime PostedDateTime { get; set; }

        public int CommentCount { get; set; }
    }
}
