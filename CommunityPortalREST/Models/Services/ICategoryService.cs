using System.Collections.Generic;
using CommunityPortalREST.Models.Domains;
using CommunityPortalREST.Models.ViewModels.Category;

namespace CommunityPortalREST.Models.Services
{
    public interface ICategoryService : IService<Category, CreateCategoryViewModel>
    {
        Category MakeAsMenu(int id);
        Category ReleaseFromMenu(int id);
        List<Category> Menus();

    }
}
