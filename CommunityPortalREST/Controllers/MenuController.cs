using System.Collections.Generic;
using System.Linq;
using CommunityPortalREST.Models.Domains;
using CommunityPortalREST.Models.Services;
using CommunityPortalREST.Models.ViewModels.Menu;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CommunityPortalREST.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MenuController : ControllerBase
    {
        private CategoryService Service { get; }

        public MenuController(CategoryService service)
        {
            Service = service;
        }

        [HttpGet]
        public ActionResult<List<MenuViewModel>> Index()
        {
            return Service.Menus()
                .Select(item => new MenuViewModel()
                {
                    Id = item.Id,
                    Title = item.Title
                })
                .ToList();
        }

        [HttpPost]
        [Authorize]
        public ActionResult<MenuViewModel> Create(int id)
        {
            Category entry = Service.MakeAsMenu(id);

            if (entry == null)
            {
                return BadRequest();
            }

            return new MenuViewModel()
            {
                Id = entry.Id,
                Title = entry.Title
            };
        }

        [HttpDelete("{id:int}")]
        [Authorize]
        public ActionResult Delete(int id)
        {
            Category entry = Service.ReleaseFromMenu(id);

            if (entry == null)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
