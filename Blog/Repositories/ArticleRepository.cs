using Blog.Data;
using Blog.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Blog.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly BlogDbContext blogDbContext;

        public ArticleRepository(BlogDbContext blogDbContext)
        {
            this.blogDbContext = blogDbContext;
        }
        public async Task<Article> AddAsync(Article article)
        {
            await blogDbContext.AddAsync(article);
            await blogDbContext.SaveChangesAsync();
            return article;
        }

        public async Task<Article?> DeleteAsync(Guid id)
        {
            var existingArticle = await blogDbContext.Articles.FindAsync(id);

            if (existingArticle != null)
            {
                blogDbContext.Articles.Remove(existingArticle);
                await blogDbContext.SaveChangesAsync();
                return existingArticle;
            }

            return null;

        }

        public async Task<IEnumerable<Article>> GetAllAsync()
        {
            return await blogDbContext.Articles.Include(x => x.Tags).ToListAsync();
        }

        public async Task<IEnumerable<Article>> GetArticlesByAuthorAsync(string userName)
        {
            return await blogDbContext.Articles.Include(x => x.Tags).Where(x => x.Author == userName).ToListAsync();
        }

        public async Task<Article?> GetAsync(Guid id)
        {
            return await blogDbContext.Articles.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Article?> GetByUrlHandleAsync(string urlHandle)
        {
            return await blogDbContext.Articles.Include(x => x.Tags).FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
        }

        public async Task<Article?> UpdateAsync(Article article)
        {
            var existingArticle = await blogDbContext.Articles.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == article.Id);

            if (existingArticle != null)
            {
                existingArticle.Id = article.Id;
                existingArticle.Heading = article.Heading;
                existingArticle.PageTitle = article.PageTitle;
                existingArticle.Content = article.Content;
                existingArticle.ShortDescription = article.ShortDescription;
                existingArticle.FeaturedImageUrl = article.FeaturedImageUrl;
                existingArticle.UrlHandle = article.UrlHandle;
                existingArticle.PublishedDate = article.PublishedDate;
                existingArticle.Author = article.Author;
                existingArticle.Visible = article.Visible;
                existingArticle.Tags = article.Tags;

                await blogDbContext.SaveChangesAsync();
                return existingArticle;
            }

            return null;
        }
    }
}

