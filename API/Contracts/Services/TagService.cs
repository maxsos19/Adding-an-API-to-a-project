using API.Contracts.Services.IServices;
using API.Data;
using API.Data.Models.Request.Tags;
using API.Data.Models.Response.Tags;
using API.Repositories;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Contracts.Services
{
    public class TagService : ITagService
    {

        private readonly ITagRepository _repo;
        private readonly BlogDbContext _blogDbContext;

        public TagService(ITagRepository repo, BlogDbContext blogDbContext)
        {
            _repo = repo;
            _blogDbContext = blogDbContext;
        }

        public Task<Tag> AddAsync(TagCreateRequest request)
        {

            ValidateAddTagViewModel(request);

            var tag = new Tag
            {
                Name = request.Name,
                DisplayName = request.DisplayName
            };
            return _repo.AddAsync(tag);
        }

        public async Task<int> CountAsync()
        {
            return await _blogDbContext.Tags.CountAsync();
        }

        public Task<Tag?> DeleteAsync(Guid id)
        {
            return _repo.DeleteAsync(id);
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
           
            var tags = await _repo.GetAllAsync();

            return tags;
        }

        public Task<Tag?> GetAsync(Guid id)
        {
            return _repo.GetAsync(id);
        }

        public Task<Tag?> UpdateAsync(TagEditRequest request)
        {

            var tag = new Tag
            {
                Id = request.Id,
                Name = request.Name,
                DisplayName = request.DisplayName
            };

            var updatedTag = _repo.UpdateAsync(tag);

            return updatedTag;
        }

        private void ValidateAddTagViewModel(TagCreateRequest request)
        {
            if (request.Name is not null && request.DisplayName is not null)
            {
                if (request.Name == request.DisplayName)
                {
                    Console.WriteLine("DisplayName", "Name cannot be the same as DisplayName");
                }
            }
        }

    }

    
}
