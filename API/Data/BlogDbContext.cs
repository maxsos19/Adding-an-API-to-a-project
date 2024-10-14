
using API.Data.Models.Response.Articles;
using API.Data.Models.Response.ArticlesLikes;
using API.Data.Models.Response.Comments;
using API.Data.Models.Response.Tags;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ArticleLike> ArticlesLikes { get; set; }
        public DbSet<ArticlesComment> Comments { get; set; }
    }
}
