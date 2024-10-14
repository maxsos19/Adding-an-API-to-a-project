using Blog.Models;
using Blog.Models.ViewModels;
using Blog.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IArticleRepository articleRepository;
        private readonly ITagRepository tagRepository;

        public HomeController(ILogger<HomeController> logger, IArticleRepository articleRepository, ITagRepository tagRepository)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog подключен к HomeController");
            this.articleRepository = articleRepository;
            this.tagRepository = tagRepository;
        }

        public async Task<IActionResult> Index()
        {
            var articles = await articleRepository.GetAllAsync();

            var tags = await tagRepository.GetAllAsync();

            var model = new HomeViewModel
            {
                Articles = articles,
                Tags = tags
            };
            _logger.LogInformation("HomeController - обращение к методу Index");
            return View(model);
        }


        public IActionResult Privacy()
        {
            _logger.LogInformation("HomeController - обращение к методу Privacy");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            _logger.LogError(Activity.Current?.Id, "Server error");
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
