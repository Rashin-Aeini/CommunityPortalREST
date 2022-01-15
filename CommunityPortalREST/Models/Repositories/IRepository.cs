using System.Collections.Generic;

namespace CommunityPortalREST.Models.Repositories
{
    public interface IRepository<T>
    {
        List<T> Read();
        T Read(int id);
        T Create(T entry);
        bool Update(T entry);
        bool Delete(T entry);
    }
}
