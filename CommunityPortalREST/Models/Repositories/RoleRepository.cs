using System.Collections.Generic;
using System.Linq;
using CommunityPortalREST.Models.Data;
using CommunityPortalREST.Models.Domains;

namespace CommunityPortalREST.Models.Repositories
{
    public class RoleRepository : IRepository<Role>
    {
        private PortalContext Context { get; }

        public RoleRepository(PortalContext context)
        {
            Context = context;
        }

        public List<Role> Read()
        {
            return Context.Roles.ToList();
        }

        public Role Read(int id)
        {
            return Read().SingleOrDefault(role => role.Id == id);
        }

        public Role Create(Role entry)
        {
            Context.Roles.Add(entry);

            Context.SaveChanges();

            return entry;
        }

        public bool Update(Role entry)
        {
            Context.Roles.Update(entry);

            return Context.SaveChanges() > 0;
        }

        public bool Delete(Role entry)
        {
            Context.Roles.Remove(entry);

            return Context.SaveChanges() > 0;
        }
    }
}
