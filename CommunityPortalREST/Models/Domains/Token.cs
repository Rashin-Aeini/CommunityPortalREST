using System;

namespace CommunityPortalREST.Models.Domains
{
    public class Token
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public DateTime Expire { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
    }
}
