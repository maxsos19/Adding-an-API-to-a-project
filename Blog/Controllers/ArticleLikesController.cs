using Blog.Models.Domain;
using Blog.Models.ViewModels;
using Blog.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleLikesController : ControllerBase
    {
        private readonly IArticleLikesRepository articleLikesRepository;
        private readonly ILogger<ArticleLikesController> _logger;

        public ArticleLikesController(IArticleLikesRepository articleLikesRepository, ILogger<ArticleLikesController> logger)
        {
            this.articleLikesRepository = articleLikesRepository;
            _logger = logger;
            _logger.LogDebug(1, "NLog подключен к ArticleLikesController");
        }


        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddLike([FromBody] AddLikeViewModel addLikeViewModel)
        {
            var model = new ArticleLike
            {
                ArticleId = addLikeViewModel.ArticleId,
                UserId = addLikeViewModel.UserId
            };

            await articleLikesRepository.AddLikeForArticle(model);
            _logger.LogInformation("ArticleLikesController - обращение к методу Add");
            return Ok();
        }


        [HttpGet]
        [Route("{articleId:Guid}/totalLikes")]
        public async Task<IActionResult> GetTotalLikesForArticle([FromRoute] Guid articleId)
        {
           var totalLikes = await articleLikesRepository.GetTotalLikes(articleId);
            _logger.LogInformation("ArticleLikesController - обращение к методу GetTotalLikesForArticle");
            return Ok(totalLikes);
        }
    }
}
