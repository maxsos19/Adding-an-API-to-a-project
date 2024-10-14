using Blog.Models.Domain;
using Blog.Models.ViewModels;
using Blog.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TagsController : Controller
    {

        private readonly ITagRepository tagRepository;
        private readonly ILogger<TagsController> _logger;

        public TagsController(ITagRepository tagRepository, ILogger<TagsController> logger)
        {
            this.tagRepository = tagRepository;
            _logger = logger;
            _logger.LogDebug(1, "NLog подключен к TagsController");
        }

        
        [HttpGet]
        public IActionResult Add()
        {
            _logger.LogInformation("TagsController - обращение к методу Add");
            return View();
        }

       
        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddTagViewModel addTagViewModel)
        {
            ValidateAddTagViewModel(addTagViewModel);

            if (ModelState.IsValid == false)
            {
                return View();
            }

            // Mapping AddTagViewModel to Tag domain model
            var tag = new Tag
            {
                Name = addTagViewModel.Name,
                DisplayName = addTagViewModel.DisplayName
            };

            await tagRepository.AddAsync(tag);
            _logger.LogInformation("TagsController - обращение к методу Add");

            return RedirectToAction("List");
        }


        
        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List(string? searchQuery, string? sortBy, string? sortDirection, int pageSize = 3, int pageNumber = 1)
        {

            var totalRecords = await tagRepository.CountAsync();

            var totalPages = Math.Ceiling((decimal)totalRecords / pageSize);

            if (pageNumber > totalPages)
            {
                pageNumber--;
            }

            if (pageNumber < 1)
            {
                pageNumber++;
            }

            ViewBag.TotalPages = totalPages;

            ViewBag.SearchQuery = searchQuery;
            ViewBag.SortBy = sortBy;
            ViewBag.SortDirection = sortDirection;
            ViewBag.PageSize = pageSize;
            ViewBag.PageNumber = pageNumber;

            var tags = await tagRepository.GetAllAsync(searchQuery, sortBy, sortDirection, pageNumber, pageSize);
            _logger.LogInformation("TagsController - обращение к методу List");
            return View(tags);
        }

       
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var tag = await tagRepository.GetAsync(id);

            if (tag != null)
            {
                var editTagViewModel = new EditTagViewModel
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };
                _logger.LogInformation("TagsController - обращение к методу Edit");
                return View(editTagViewModel);
            }

            return View(null);
        }


        
        [HttpPost]
        public async Task<IActionResult> Edit(EditTagViewModel editTagViewModel)
        {
            var tag = new Tag
            {
                Id = editTagViewModel.Id,
                Name = editTagViewModel.Name,
                DisplayName = editTagViewModel.DisplayName
            };

            var updatedTag = await tagRepository.UpdateAsync(tag);

            if (updatedTag != null)
            {
                _logger.LogInformation("TagsController - обращение к методу Edit");
                return RedirectToAction("List", "Tags");
            }
           
            return RedirectToAction("Edit", new { id = editTagViewModel.Id });
        }



        
        [HttpPost]
        public async Task<IActionResult> Delete(EditTagViewModel editTagViewModel)
        {
            var deletedTag = await tagRepository.DeleteAsync(editTagViewModel.Id);

            if (deletedTag != null)
            {
                _logger.LogInformation("TagsController - обращение к методу Delete");
                return RedirectToAction("List", "Tags");
            }

            
            return RedirectToAction("Edit", new { id = editTagViewModel.Id });
        }

        private void ValidateAddTagViewModel(AddTagViewModel addTagViewModel)
        {
            if(addTagViewModel.Name is not null && addTagViewModel.DisplayName is not null)
            {
                if (addTagViewModel.Name == addTagViewModel.DisplayName)
                {
                    ModelState.AddModelError("DisplayName", "Name cannot be the same as DisplayName");
                }
            }
        }
    }
}
