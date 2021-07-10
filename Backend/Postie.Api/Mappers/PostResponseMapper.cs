using System.Collections.Generic;
using System.Linq;
using Postie.Api.Models.Db;
using Postie.Api.Repositories.Interfaces;

namespace Postie.Api.Mappers
{
    public class PostResponseMapper
    {
        private readonly IPostVotesRepository _postVotesRepository;

        private readonly ICommentRepository _commentRepository;

        public PostResponseMapper(IPostVotesRepository postVotesRepository, ICommentRepository commentRepository)
        {
            _postVotesRepository = postVotesRepository;
            _commentRepository = commentRepository;
        }

        public IEnumerable<PostApiResponse> MapDbToResponseList(IEnumerable<Post> posts)
        {
            return posts.Select(MapDbToResponse).ToList();
        }

        public PostApiResponse MapDbToResponse(Post post)
        {
            return new PostApiResponse
            {
                ID = post.ID,
                Board = post.Board.Url ?? "",
                Title = post.Title ?? "",
                CreatedBy = post.CreatedBy.Username ?? "",
                CreatedDateTime = post.CreatedDateTime,
                Content = post.Content,
                Url = post.Url,
                CommentCount = _commentRepository.GetCommentsCountForPostId(post.ID),
                UpVotes = _postVotesRepository.GetPostVotes(post.ID).UpVotes
            };
        }
    }
}
