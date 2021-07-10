using System.Collections.Generic;
using Postie.Api.Models.Db;

namespace Postie.Api.Repositories.Interfaces
{
    public interface IPostRepository
    {
        IEnumerable<Post> GetPostsForBoard(Board board);

        Post GetPostByBoardAndUrl(string boardUrl, string postUrl);

        Post GetPostById(int postId);

        IEnumerable<Post> GetLastPostsByUser(User user, int amount);

        bool AddPost(Post post);

        IEnumerable<Post> GetTopPosts(int skip = 0, int take = 10);
    }
}
