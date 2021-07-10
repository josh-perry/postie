using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Postie.Api.Data;
using Postie.Api.Models.Db;
using Postie.Api.Repositories.Interfaces;

namespace Postie.Api.Repositories
{

    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PostRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Post> GetPostsForBoard(Board board)
        {
            return _dbContext.Posts
                .Include(x => x.CreatedBy)
                .Where(x => x.Board == board);
        }

        public Post GetPostByBoardAndUrl(string boardUrl, string postUrl)
        {
            var board = _dbContext.Boards.FirstOrDefault(x => x.Url == boardUrl);
            var post = _dbContext.Posts
                .Include(x => x.CreatedBy)
                .FirstOrDefault(x => x.Board == board && x.Url == postUrl);

            return post;
        }

        public Post GetPostById(int postId)
        {
            return _dbContext.Posts
                .Include(x => x.Board)
                .Include(x => x.CreatedBy)
                .FirstOrDefault(x => x.ID == postId);
        }

        public IEnumerable<Post> GetLastPostsByUser(User user, int amount)
        {
            return _dbContext.Posts
                .Include(x => x.Board)
                .Where(x => x.CreatedBy == user)
                .OrderByDescending(x => x.CreatedDateTime)
                .Take(amount)
                .ToList();
        }

        public bool AddPost(Post post)
        {
            _dbContext.Add(post);
            return _dbContext.SaveChanges() > 0;
        }

        public IEnumerable<Post> GetTopPosts(int skip = 0, int take = 10)
        {
            var ids = _dbContext.PostVotes
                .Where(x => x.Up)
                .GroupBy(x => x.PostID)
                .OrderByDescending(x => x.Count())
                .Skip(skip)
                .Take(take)
                .Select(x => x.Key);

            var posts = new List<Post>();

            foreach (var postId in ids)
            {
                posts.Add(GetPostById(postId));
            }

            return posts;
        }
    }
}
