using API.Contracts.Services.IServices;
using API.Data.Models.Request.Articles;
using API.Data.Models.Response.Articles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ArticlesController : Controller
    {
        private readonly IArticleService _articleService;
        public ArticlesController(IArticleService articleService)
        {
            _articleService = articleService;
        }

       
        [HttpPost]
        [Route("AddArticle")]
        public async Task<IActionResult> Add(ArticleCreateRequest request)
        {

            var result = await _articleService.AddAsync(request);
            return StatusCode(201, result);
        }

        [HttpGet]
        [Route("GetArticles")]
        public async Task<IEnumerable<Article>> List()
        {
            var articles = await _articleService.GetAllAsync();
           
            return articles;
        }

        [HttpPatch]
        [Route("UpdateArticle")]
        public async Task<IActionResult> Edit(ArticleEditRequest request)
        {

            var updatedArticle = await _articleService.UpdateAsync(request);

            return StatusCode(201, updatedArticle);
        }

        [HttpDelete]
        [Route("DeleteArticle")]
        public async Task<IActionResult> DeleteArticle(ArticleEditRequest request)
        {
            var deletedArticle = await _articleService.DeleteAsync(request);

            return StatusCode(201, deletedArticle);
        }


        
    }
}

