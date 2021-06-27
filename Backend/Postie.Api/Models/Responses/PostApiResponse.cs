using System;

namespace Postie.Api
{
    public class PostApiResponse
    {
        public string Title { get; set; }

        public string CreatedBy { get; set; }

        public string Board { get; set; }

        public DateTime CreatedDateTime { get; set; }
        
        public string Url { get; set; }
    }
}
