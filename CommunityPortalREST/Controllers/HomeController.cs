using System;
using System.Collections.Generic;
using System.Linq;
using CommunityPortalREST.Models.Domains;
using CommunityPortalREST.Models.Services;
using CommunityPortalREST.Models.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;

namespace CommunityPortalREST.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        private PostService Service { get; }

        public HomeController(PostService service)
        {
            Service = service;
        }

        [HttpGet]
        public ActionResult<ResponseViewModel> Index(
            [FromQuery] RequestViewModel entry
            )
        {
            List<Post> posts = Service.GetAll();

            if (entry.Category > 0)
            {
                posts = posts
                    .Where(post => post.Categories
                        .Any(categoryPost => categoryPost.CategoryId == entry.Category))
                    .ToList();
            }

            if (entry.Page <= 0)
            {
                entry.Page = 1;
            }

            if (entry.Size <= 0)
            {
                entry.Page = 10;
            }

            int offset = entry.Page > 1 ? (entry.Page - 1) * entry.Size : 0;

            ResponseViewModel response = new ResponseViewModel()
            {
                Pages = (int) Math.Ceiling(posts.Count / (double) entry.Size),
                Posts = posts
                    .OrderBy(post => post.Id)
                    .Skip(offset)
                    .Take(entry.Size)
                    .Select(post => new PostViewModel()
                    {
                        Title = post.Title,
                        Content = post.Content,
                        Thumbnail = post.Thumbnail,
                        Categories = post.Categories
                            .Select(category => new CategoryViewModel()
                            {
                                Id = category.CategoryId,
                                Title = category.Category.Title
                            })
                            .ToList()
                    })
                    .ToList()
            };

            return response;
        }
    }
}
