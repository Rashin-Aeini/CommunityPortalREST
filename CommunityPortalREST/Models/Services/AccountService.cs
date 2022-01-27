using System;
using System.Collections.Generic;
using CommunityPortalREST.Models.Domains;
using CommunityPortalREST.Models.Repositories;
using CommunityPortalREST.Models.ViewModels.Account;

namespace CommunityPortalREST.Models.Services
{
    public class AccountService : IAccountService
    {
        private AccountRepository Repository { get; }
        private TokenRepository TokenRepository { get; }

        public AccountService(AccountRepository repository, TokenRepository token)
        {
            Repository = repository;
            TokenRepository = token;
        }

        public List<Account> GetAll()
        {
            return Repository.Read();
        }

        public Account GetById(int id)
        {
            return Repository.Read(id);
        }

        public Account Add(RequestLoginViewModel entry)
        {
            Account model = new Account()
            {
                Username = entry.Username,
                Password = entry.Password
            };

            return Repository.Create(model);
        }

        public bool Edit(int id, RequestLoginViewModel entry)
        {
            Account model = new Account()
            {
                Id = id,
                Username = entry.Username,
                Password = entry.Password
            };

            return Repository.Update(model);
        }

        public bool Remove(int id)
        {
            Account model = GetById(id);

            return model != null && Repository.Delete(model);
        }

        public string GenerateToken(int id, DateTime expire)
        {
            string result = null;

            Account model = GetById(id);

            if (model != null)
            {
                try
                {
                    do
                    {
                        result = Guid.NewGuid().ToString().Replace("-", "");
                    } while (TokenRepository.Valid(result));

                    if (model.Tokens == null)
                    {
                        model.Tokens = new List<Token>();
                    }

                    Token token = new Token()
                    {
                        AccountId = model.Id,
                        Number = result,
                        Expire = expire
                    };

                    model.Tokens.Add(token);

                    Repository.Update(model);
                }
                catch (Exception e)
                {
                    result = null;
                }
            }

            return result;
        }

        public bool AppendToRole(int account, int role)
        {
            throw new NotImplementedException();
        }

        public bool ReleaseFromRole(int account, int role)
        {
            throw new NotImplementedException();
        }
    }
}
