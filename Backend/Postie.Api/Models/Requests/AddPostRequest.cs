namespace Postie.Api.Models.Requests
{
    public class AddPostRequest
    {
        public string Title { get; set; }
        
        public string Content { get; set; }
        
        public string Board { get; set; }
    }
}
