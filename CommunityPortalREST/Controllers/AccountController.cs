using System;
using System.Linq;
using CommunityPortalREST.Models.Domains;
using CommunityPortalREST.Models.Services;
using CommunityPortalREST.Models.ViewModels.Account;
using Microsoft.AspNetCore.Mvc;

namespace CommunityPortalREST.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        public AccountService Service { get; }

        public AccountController(AccountService service)
        {
            Service = service;
        }

        [HttpPost]
        public ActionResult<ResponseLoginViewModel> 
            Login([FromBody] RequestLoginViewModel entry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Account model = Service.GetAll()
                .SingleOrDefault(account =>
                account.Username == entry.Username && account.Password == entry.Password);

            if (model == null)
            {
                return BadRequest();
            }

            string token = Service.GenerateToken(model.Id, DateTime.Now.AddDays(7));

            if (string.IsNullOrEmpty(token))
            {
                return BadRequest();
            }

            ResponseLoginViewModel response = new ResponseLoginViewModel()
            {
                FirstName = model.Name,
                LastName = model.Family,
                Token = token
            };


            return response;
        }
    }
}
