namespace Postie.Api.Models.Requests
{
    public class AddCommentRequest
    {
        public int? ParentCommentId { get; set; }

        public string Content { get; set; }
    }
}
