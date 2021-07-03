namespace Postie.Api.Models.Responses
{
    public class CommentApiResponse
    {
        public string User { get; set; }

        public string Content { get; set; }

        public int? ParentCommentId { get; set; }
    }
}
