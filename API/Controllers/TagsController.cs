using API.Contracts.Services.IServices;
using API.Data.Models.Request.Tags;
using API.Data.Models.Response.Tags;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Admin")]
    public class TagsController : Controller
    {
        private readonly ITagService _tagSerive;
       

        public TagsController(ITagService tagService)
        {
            _tagSerive = tagService;
            
        }

       
       
        [HttpGet]
        [Route("All")]
        public async Task<IEnumerable<Tag>> List()
        {
            var tags = await _tagSerive.GetAllAsync();
            return tags;
        }

       
        
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddTag(TagCreateRequest request)
        {
            var result = await _tagSerive.AddAsync(request);
            return StatusCode(201, result);
        }

       
        
        [HttpPatch]
        [Route("Update")]
        public async Task<IActionResult> EditTag(TagEditRequest request)
        {
            var updatedTag = await _tagSerive.UpdateAsync(request);

            return StatusCode(201, updatedTag);
        }

       
        
        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> RemoveTag(TagEditRequest request)
        {
            var deletedTag = await _tagSerive.DeleteAsync(request.Id);

            return StatusCode(201, deletedTag);
        }


       
    }
}
