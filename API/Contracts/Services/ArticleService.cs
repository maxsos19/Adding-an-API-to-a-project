using API.Contracts.Services.IServices;
using API.Data.Models.Request.Articles;
using API.Data.Models.Response.Articles;
using API.Data.Models.Response.Tags;
using API.Data.Models.Response.Users;
using API.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Contracts.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _repo;
        private readonly ITagRepository _tagRepo;
        //private readonly UserManager<User> _userManager;
        private readonly ICommentRepository _commentRepo;


        public ArticleService(ITagRepository tagRepo, IArticleRepository repo, ICommentRepository commentRepo)
        {
            _repo = repo;
            _tagRepo = tagRepo;
           
           // _userManager = userManager;
            _commentRepo = commentRepo;
        }

        public async Task<Article> AddAsync(ArticleCreateRequest request)
        {
            var article = new Article
            {
                Heading = request.Heading,
                PageTitle = request.PageTitle,
                Content = request.Content,
                ShortDescription = request.ShortDescription,
                FeaturedImageUrl = request.FeaturedImageUrl,
                UrlHandle = request.UrlHandle,
                PublishedDate = request.PublishedDate,
                Author = request.Author,
                Visible = request.Visible,
            };


            var selectedTags = new List<Tag>();

            foreach (var selectedTagId in request.SelectedTags)
            {
                var selectedTagIdAsGuid = Guid.Parse(selectedTagId);
                var existingTag = await _tagRepo.GetAsync(selectedTagIdAsGuid);

                if (existingTag != null)
                {
                    selectedTags.Add(existingTag);
                }
            }

            article.Tags = selectedTags;

            await _repo.AddAsync(article);

            return article;
        }

        public async Task<Article?> DeleteAsync(ArticleEditRequest request)
        {
            var deletedArticle = await _repo.DeleteAsync(request.Id);

            return deletedArticle;
        }

        public async Task<IEnumerable<Article>> GetAllAsync()
        {
            var articles = await _repo.GetAllAsync();
            
            return articles;
        }

        public Task<Article?> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Article?> UpdateAsync(ArticleEditRequest request)
        {
            var article = new Article
            {
                Id = request.Id,
                Heading = request.Heading,
                PageTitle = request.PageTitle,
                Content = request.Content,
                ShortDescription = request.ShortDescription,
                FeaturedImageUrl = request.FeaturedImageUrl,
                UrlHandle = request.UrlHandle,
                PublishedDate = request.PublishedDate,
                Author = request.Author,
                Visible = request.Visible
            };

            var selectedTags = new List<Tag>();
            foreach (var selectedTag in request.SelectedTags)
            {
                if (Guid.TryParse(selectedTag, out var tag))
                {
                    var foundTag = await _tagRepo.GetAsync(tag);

                    if (foundTag != null)
                    {
                        selectedTags.Add(foundTag);
                    }
                }

            }

            article.Tags = selectedTags;

            var updatedArticle = await _repo.UpdateAsync(article);

           return updatedArticle;
           
        }


       
    }
}
