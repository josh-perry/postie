using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using Bogus.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Postie.Api.Data;
using Postie.Api.Models.Db;
using Postie.Api.Models.Options;

namespace Postie.Api
{
    public static class DatabaseSeeder
    {
        private const int UserCount = 50;

        private const int MinBoards = 5;

        private const int MaxBoards = 12;

        private const int MinPostsPerUser = 1;

        private const int MaxPostsPerUser = 50;

        private const int MinCommentsPerUser = 70;

        private const int MaxCommentsPerUser = 100;

        private const int CommentReplyChance = 75;

        private static readonly Random _random = new Random();

        public static void SeedDatabase(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var defaultUserOptions = serviceScope.ServiceProvider.GetRequiredService<SeedDataOptions>();

            // Apply any migrations, but don't seed if not necessary or not in development mode
            dbContext.Database.Migrate();

            if (!env.IsDevelopment())
                return;

            if (dbContext.Users.FirstOrDefault() != null)
                return;

            foreach (var user in defaultUserOptions.Users)
            {
                var defaultUser = new User
                {
                    Username = user.Username,
                    AuthId = user.AuthId
                };

                dbContext.Users.Add(defaultUser);
            }

            dbContext.SaveChanges();

            var userFaker = new Faker<User>()
                .RuleFor(u => u.Username, f => f.Internet.UserName())
                .RuleFor(u => u.AuthId, f => "auth0|");

            // Create users
            var users = userFaker.Generate(UserCount);
            dbContext.Users.AddRange(users);
            dbContext.SaveChanges();

            var boards = new List<Board>();
            var posts = new List<Post>();
            var comments = new List<Comment>();

            // Create boards
            var boardFaker = new Faker<Board>()
                .RuleFor(b => b.Description, f => f.Lorem.Paragraphs())
                .RuleFor(b => b.Title, f => f.Lorem.Sentence(1, 3).ToString())
                .RuleFor(b => b.Url, (f, b) => b.Title.Replace(" ", "-").ToLower().Replace(".", ""))
                .RuleFor(b => b.CreatedBy, f => f.PickRandom(users))
                .RuleFor(b => b.CreatedDateTime, f => f.Date.Recent());

            boards.AddRange(boardFaker.GenerateBetween(MinBoards, MaxBoards));
            dbContext.Boards.AddRange(boards);

            dbContext.SaveChanges();

            // Create posts
            foreach (var user in users)
            {
                var postFaker = new Faker<Post>()
                    .RuleFor(p => p.Board, f => f.PickRandom(boards))
                    .RuleFor(p => p.Content, f => f.Lorem.Paragraphs(1, 5))
                    .RuleFor(p => p.Title, f => f.Lorem.Sentence())
                    .RuleFor(p => p.Url, (f, p) => p.Title.Replace(" ", "-").ToLower().Replace(".", ""))
                    .RuleFor(p => p.CreatedBy, f => user)
                    .RuleFor(p => p.CreatedDateTime, f => f.Date.Recent());

                var postsForUser = postFaker.GenerateBetween(MinPostsPerUser, MaxPostsPerUser);
                posts.AddRange(postsForUser);
                dbContext.Posts.AddRange(postsForUser);

                dbContext.SaveChanges();
            }

            // Create comments
            foreach (var user in users)
            {
                var commentFaker = new Faker<Comment>()
                    .RuleFor(c => c.CreatedBy, f => user)
                    .RuleFor(c => c.Content, f => f.Lorem.Paragraphs(1, 4))
                    .RuleFor(c => c.Post, f => f.PickRandom(posts))
                    .RuleFor(c => c.CreatedDateTime, f => f.Date.Recent());

                var userComments = commentFaker.GenerateBetween(MinCommentsPerUser, MaxCommentsPerUser);
                comments.AddRange(userComments);

                foreach (var comment in userComments)
                {
                    if (_random.Next(100) > CommentReplyChance)
                        continue;

                    var parentComment = comments[_random.Next(comments.Count)];
                    if (parentComment == comment)
                        continue;

                    if (CheckCommentTreeContainsAncestor(parentComment, comment))
                        continue;

                    comment.ParentComment = parentComment;
                    comment.Post = parentComment.Post;
                }

                dbContext.Comments.AddRange(userComments);

                dbContext.SaveChanges();
            }
        }

        private static bool CheckCommentTreeContainsAncestor(Comment commentToCheck, Comment possibleAncestor)
        {
            var ancestor = commentToCheck;

            while (ancestor != null)
            {
                if (ancestor == possibleAncestor)
                    return true;

                ancestor = ancestor.ParentComment;
            }

            return false;
        }
    }
}
