using System;
using System.Collections.Generic;
using System.Linq;
using CommunityPortalREST.Models.Domains;
using CommunityPortalREST.Models.Services;
using CommunityPortalREST.Models.ViewModels.Post;
using Microsoft.AspNetCore.Mvc;

namespace CommunityPortalREST.Controllers
{
    [ApiController]
    [Route("[controller]")] // => /post
    public class PostController : ControllerBase
    {
        private PostService Service { get; }

        public PostController(PostService service)
        {
            Service = service;
        }

        [HttpGet] 
        public ActionResult<ResponsePostViewModel> Index(
            [FromQuery]RequestFilterPostViewModel entry)
        {
            const int size = 20;
            
            List<Post> posts = Service.GetAll();
            
            if (!string.IsNullOrEmpty(entry.Search))
            {
                posts = posts.Where(item => item.Title.Contains(entry.Search)).ToList();
            }

            if (!string.IsNullOrEmpty(entry.Sort) && !string.IsNullOrEmpty(entry.Type))
            {
                if (entry.Sort.Equals(nameof(IndexPostViewModel.Id)))
                {
                    if (entry.Type.Equals("Asc"))
                    {
                        posts = posts.OrderBy(item => item.Id).ToList();
                    }
                    else if (entry.Type.Equals("Desc"))
                    {
                        posts = posts.OrderByDescending(item => item.Id).ToList();
                    }
                }
                else if (entry.Sort.Equals(nameof(IndexPostViewModel.Title)))
                {
                    if (entry.Type.Equals("Asc"))
                    {
                        posts = posts.OrderBy(item => item.Title).ToList();
                    }
                    else if (entry.Type.Equals("Desc"))
                    {
                        posts = posts.OrderByDescending(item => item.Title).ToList();
                    }
                }
                else if (entry.Sort.Equals(nameof(IndexPostViewModel.Created)))
                {
                    if (entry.Type.Equals("Asc"))
                    {
                        posts = posts.OrderBy(item => item.Created).ToList();
                    }
                    else if (entry.Type.Equals("Desc"))
                    {
                        posts = posts.OrderByDescending(item => item.Created).ToList();
                    }
                }
            }
            

            ResponsePostViewModel response = new ResponsePostViewModel
            {
                Pages = (int)Math.Ceiling(posts.Count / (double)size)
            };

            int offset = entry.Page > 1 ? (entry.Page - 1) * size : 0;

            response.Result= posts
                .Skip(offset)
                .Take(size)
                .Select(item => new IndexPostViewModel
                {
                    Id = item.Id,
                    Title = item.Title,
                    Created = DateTime.Now.Subtract(item.Created)
                })
                .ToList();


            return response;
        }

        [HttpPost] // /post
        public ActionResult<Post> Create(CreatePostViewModel entry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(entry);
            }

            Post post = Service.Add(entry);

            return post;
        }
    }
}
