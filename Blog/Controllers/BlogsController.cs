using Blog.Models.Domain;
using Blog.Models.ViewModels;
using Blog.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IArticleRepository articleRepository;
        private readonly IArticleLikesRepository articleLikesRepository;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly ICommentRepository commentRepository;
        private readonly ILogger<BlogsController> _logger;

        public BlogsController(IArticleRepository articleRepository, IArticleLikesRepository articleLikesRepository, SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager, ICommentRepository commentRepository, ILogger<BlogsController> logger)
        {
            this.articleRepository = articleRepository;
            this.articleLikesRepository = articleLikesRepository;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.commentRepository = commentRepository;
            _logger = logger;
            _logger.LogDebug(1, "NLog подключен к BlogsController");
        }

        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var liked = false;
            var article = await articleRepository.GetByUrlHandleAsync(urlHandle);
            var articleDetailsViewModel = new ArticleDetailsViewModel();

            if (article != null)
            {
                var totalLikes = await articleLikesRepository.GetTotalLikes(article.Id);

                if (signInManager.IsSignedIn(User))
                {
                    var likesForArticle = await articleLikesRepository.GetLikesForArticle(article.Id);

                    var userId = userManager.GetUserId(User);

                    if(userId != null)
                    {
                        var likeFromUser = likesForArticle.FirstOrDefault(x => x.UserId == Guid.Parse(userId));

                       liked = likeFromUser != null;
                    }
                }


                // Get comments for blog post
                var articleComments = await commentRepository.GetCommentsByArticleIdAsync(article.Id);

                var commentsForView = new List<CommentViewModel>();

                foreach (var comment in articleComments)
                {
                    commentsForView.Add(new CommentViewModel
                    {
                        Description = comment.Description,
                        DateAdded = comment.DateAdded,
                        Username = (await userManager.FindByIdAsync(comment.UserId.ToString())).UserName
                    });
                }

                articleDetailsViewModel = new ArticleDetailsViewModel

                {
                    Id = article.Id,
                    Content = article.Content,
                    PageTitle = article.PageTitle,
                    Author = article.Author,
                    FeaturedImageUrl = article.FeaturedImageUrl,
                    Heading = article.Heading,
                    PublishedDate = article.PublishedDate,
                    ShortDescription = article.ShortDescription,
                    UrlHandle = article.UrlHandle,
                    Visible = article.Visible,
                    Tags = article.Tags,
                    TotalLikes = totalLikes,
                    Liked = liked,
                    Comments = commentsForView
                };
            }
            _logger.LogInformation("BlogsController - обращение к методу Index");
            return View(articleDetailsViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Index(ArticleDetailsViewModel articleDetailsViewModel)
        {
            if (signInManager.IsSignedIn(User))
            {
                var domainModel = new ArticlesComment
                {
                    ArticleId = articleDetailsViewModel.Id,
                    Description = articleDetailsViewModel.CommentDescription,
                    UserId = Guid.Parse(userManager.GetUserId(User)),
                    DateAdded = DateTime.Now
                };


                await commentRepository.AddAsync(domainModel);
                _logger.LogInformation("BlogsController - обращение к методу Index");
                return RedirectToAction("Index", "Blogs",
                    new {urlHandle = articleDetailsViewModel.UrlHandle});
            }
            _logger.LogInformation("BlogsController - обращение к методу Index");
            return View();
        }
    }
}
