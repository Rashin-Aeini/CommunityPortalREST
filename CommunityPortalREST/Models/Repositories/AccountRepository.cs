using System.Collections.Generic;
using System.Linq;
using CommunityPortalREST.Models.Data;
using CommunityPortalREST.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace CommunityPortalREST.Models.Repositories
{
    public class AccountRepository : IRepository<Account>
    {
        private PortalContext Context { get; }

        public AccountRepository(PortalContext context)
        {
            Context = context;
        }

        public List<Account> Read()
        {
            return Context.Accounts
                .Include(account => account.Tokens)
                .Include(account => account.Roles)
                .ThenInclude(role => role.Role)
                .ToList();

        }

        public Account Read(int id)
        {
            return Read().SingleOrDefault(account => account.Id == id);
        }

        public Account Create(Account entry)
        {
            Context.Accounts.Add(entry);

            Context.SaveChanges();

            return entry;
        }

        public bool Update(Account entry)
        {
            Context.Accounts.Update(entry);

            return Context.SaveChanges() > 0;
        }

        public bool Delete(Account entry)
        {
            Context.Accounts.Remove(entry);

            return Context.SaveChanges() > 0;
        }
    }
}
