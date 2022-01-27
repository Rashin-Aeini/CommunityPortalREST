using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CommunityPortalREST.Models.Domains;
using CommunityPortalREST.Models.Repositories;
using CommunityPortalREST.Models.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CommunityPortalREST.Models.Handlers
{
    public class AuthorizationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private AccountService Service { get; }
        private TokenRepository Repository { get; }

        public AuthorizationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options, 
            ILoggerFactory logger, 
            UrlEncoder encoder, 
            ISystemClock clock, 
            AccountService service, 
            TokenRepository repository
            ) : base(options, logger, encoder, clock)
        {
            Service = service;
            Repository = repository;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Task.FromResult(AuthenticateResult.Fail("Authorization header missing."));
            }

            string header = Request.Headers["Authorization"].ToString();
            Regex regex = new Regex(@"Bearer (.*)");

            if (regex.IsMatch(header))
            {
                string token = regex.Replace(header, "$1");

                if (!string.IsNullOrEmpty(token))
                {
                    if (Repository.Read()
                        .Any(item => item.Number.Equals(token) && item.Expire > DateTime.Now))
                    {
                        int account = Repository.Read()
                            .Where(item => item.Number.Equals(token) && item.Expire > DateTime.Now)
                            .Select(item => item.AccountId)
                            .SingleOrDefault();

                        if (account != 0)
                        {
                            Account model = Service.GetById(account);

                            if (model != null)
                            {
                                ClaimsPrincipal claims = new ClaimsPrincipal(
                                    new ClaimsIdentity(
                                        new List<Claim>()
                                        {
                                            new Claim(ClaimTypes.NameIdentifier, model.Id.ToString()),
                                            new Claim(ClaimTypes.Role,
                                                string.Join(", ", model.Roles.Select(item => item.Role.Name)))
                                        }
                                    )
                                );

                                AuthenticationTicket ticket = new AuthenticationTicket(claims, Scheme.Name);

                                return Task.FromResult(
                                    AuthenticateResult.Success(ticket));
                            }
                        }
                    }

                    return Task.FromResult(AuthenticateResult.Fail("Authorization code not valid or expired."));
                }
            }

            return Task.FromResult(AuthenticateResult.Fail("Authorization code not formatted properly."));
        }
    }
}
