using System;
using System.Collections.Generic;
using System.Linq;
using CommunityPortalREST.Models.Domains;
using CommunityPortalREST.Models.Services;
using CommunityPortalREST.Models.ViewModels.Category;
using Microsoft.AspNetCore.Mvc;

namespace CommunityPortalREST.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private CategoryService Service { get; }

        public CategoryController(CategoryService service)
        {
            Service = service;
        }

        [HttpGet]
        public ActionResult<ResponseCategoryViewModel> Index(
            [FromQuery] RequestFilterCategoryViewModel entry)
        {
            List<Category> categories = Service.GetAll();

            if (entry.Size != -1 && !string.IsNullOrEmpty(entry.Search))
            {
                categories = categories
                    .Where(item => item.Title.Contains(entry.Search))
                    .ToList();
            }

            int offset = entry.Size != -1 ?
                (entry.Page > 1 ? (entry.Page - 1) * entry.Size : 0) :
                0;

            return new ResponseCategoryViewModel
            {
                Pages = entry.Size != -1 ?
                    (int)Math.Ceiling(categories.Count / (double)entry.Size) :
                    1,
                Result = categories
                    .Skip(offset)
                    .Take(entry.Size == -1 ? categories.Count : entry.Size)
                    .Select(item => new IndexCategoryViewModel()
                    {
                        Id = item.Id,
                        Title = item.Title
                    })
                    .ToList()
            };
        }

        [HttpPost]
        public ActionResult<Category> Create(CreateCategoryViewModel entry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(entry);
            }

            Category category = Service.Add(entry);

            return category;
        }

        [HttpGet("{id:int}")]
        public ActionResult<CreateCategoryViewModel> Details(int id)
        {
            Category entry = Service.GetById(id);

            if (entry == null)
            {
                return BadRequest();
            }

            CreateCategoryViewModel model = new CreateCategoryViewModel()
            {
                Title = entry.Title
            };

            return model;
        }

        [HttpPost("{id:int}")]
        public IActionResult Edit(int id, CreateCategoryViewModel entry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(entry);
            }

            if (!Service.Edit(id, entry))
            {
                return BadRequest("The category updated");
            }

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            if (!Service.Remove(id))
            {
                return BadRequest("The category delete");
            }

            return Ok();
        }
    }
}
