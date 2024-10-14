using API.Data.Models.Request.ArticlesLikes;
using API.Data.Models.Response.ArticlesLikes;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
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
            public async Task<IActionResult> AddLike([FromBody] LikeCreateRequest like)
            {
                var model = new ArticleLike
                {
                    ArticleId = like.ArticleId,
                    UserId = like.UserId
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

