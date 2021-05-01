namespace Postie.Api.Models.Requests
{
    public class PostComment
    {
        public int? ParentCommentId { get; set; }
        
        public string Content { get; set; }
    }
}
