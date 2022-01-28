using System.Collections.Generic;
using System.Linq;
using CommunityPortalREST.Models.Data;
using CommunityPortalREST.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace CommunityPortalREST.Models.Repositories
{
    public class PostRepository : IRepository<Post>
    {
        private PortalContext Context { get; }

        public PostRepository(PortalContext context)
        {
            Context = context;
        }

        public List<Post> Read()
        {
            return Context.Posts
                .Include(post => post.Categories)
                .ThenInclude(item => item.Category)
                .ToList();
        }

        public Post Read(int id)
        {
            return Read().SingleOrDefault(item => item.Id == id);
        }

        public Post Create(Post entry)
        {
            Context.Posts.Add(entry);

            Context.SaveChanges();

            return entry;
        }

        public bool Update(Post entry)
        {
            Context.Posts.Update(entry);

            return Context.SaveChanges() > 0;
        }

        public bool Delete(Post entry)
        {
            Context.Posts.Remove(entry);

            return Context.SaveChanges() > 0;
        }
    }
}
