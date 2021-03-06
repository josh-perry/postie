using System;

namespace Postie.Api
{
    public class PostApiResponse
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string CreatedBy { get; set; }

        public string Board { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public string Url { get; set; }

        public string Content { get; set; }

        public int CommentCount { get; set; }

        public int UpVotes { get; set; }
    }
}
