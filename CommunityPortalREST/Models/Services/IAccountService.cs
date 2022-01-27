using System;
using CommunityPortalREST.Models.Domains;
using CommunityPortalREST.Models.ViewModels.Account;

namespace CommunityPortalREST.Models.Services
{
    public interface IAccountService : IService<Account, RequestLoginViewModel>
    {
        string GenerateToken(int id, DateTime expire);
        bool AppendToRole(int account, int role);
        bool ReleaseFromRole(int account, int role);
    }
}
