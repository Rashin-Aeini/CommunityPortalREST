using System.Collections.Generic;
using System.Linq;
using CommunityPortalREST.Models.Data;
using CommunityPortalREST.Models.Domains;

namespace CommunityPortalREST.Models.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private PortalContext Context { get; }

        public TokenRepository(PortalContext context)
        {
            Context = context;
        }

        public bool Valid(string entry)
        {
            return Read().Any(token => token.Number.Equals(entry));
        }

        public List<Token> Read()
        {
            return Context.Tokens.ToList();
        }

        public Token Read(int id)
        {
            return Read().SingleOrDefault(token => token.Id == id);
        }

        public Token Create(Token entry)
        {
            Context.Tokens.Add(entry);

            Context.SaveChanges();

            return entry;
        }

        public bool Update(Token entry)
        {
            Context.Tokens.Update(entry);

            return Context.SaveChanges() > 0;
        }

        public bool Delete(Token entry)
        {
            Context.Tokens.Remove(entry);

            return Context.SaveChanges() > 0;
        }
    }
}
