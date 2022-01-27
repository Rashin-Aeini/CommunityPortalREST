using System;
using System.Linq;
using CommunityPortalREST.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CommunityPortalREST.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TokenController : ControllerBase
    {
        public TokenRepository Repository { get;}

        public TokenController(TokenRepository repository)
        {
            Repository = repository;
        }

        [HttpPost]
        public IActionResult Validation([FromQuery]string number)
        {
            if (!string.IsNullOrEmpty(number))
            {
                if (Repository.Read()
                    .Any(token => token.Number.Equals(number) && token.Expire > DateTime.Now))
                {
                    return Ok();
                }
            }

            return BadRequest();
        }
    }
}
