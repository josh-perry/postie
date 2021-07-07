using Microsoft.EntityFrameworkCore;
using Postie.Api.Models.Db;

namespace Postie.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Board> Boards { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<PostVote> PostVotes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().ToTable("Users");
            builder.Entity<Board>().ToTable("Boards");
            builder.Entity<Post>().ToTable("Posts");
            builder.Entity<Comment>().ToTable("Comments");
            builder.Entity<PostVote>().ToTable("PostVote");
        }
    }
}
