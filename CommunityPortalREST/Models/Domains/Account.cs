using System.Collections.Generic;

namespace CommunityPortalREST.Models.Domains
{
    public class Account
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<Token> Tokens { get; set; }
    }
}
