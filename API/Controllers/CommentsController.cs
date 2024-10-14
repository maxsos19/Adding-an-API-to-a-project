using API.Contracts.Services.IServices;
using API.Data.Models.Request.Comments;
using API.Data.Models.Response.Comments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Admin")]
    public class CommentsController : Controller
    {
        private readonly ICommentService _commentService;


        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;

        }

        [HttpGet]
        [Route("All")]
        public async Task<IEnumerable<ArticlesComment>> List()
        {
            var comments = await _commentService.GetAllAsync();

            return comments;
        }


        [HttpDelete]
        [Route("DeleteComment")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deletedComment = await _commentService.DeleteAsync(id);

            return StatusCode(201, deletedComment);
        }

       

        [HttpPatch]
        [Route("EditComment")]
        public async Task<IActionResult> Edit(CommentEditRequest request)
        {
            var result = await _commentService.UpdateAsync(request);
            if (result != null)
                return StatusCode(201, result);
            else
                return StatusCode(204);

        }


    }
}
