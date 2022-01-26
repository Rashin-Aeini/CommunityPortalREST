using System.Collections.Generic;
using System.Linq;
using CommunityPortalREST.Models.Domains;
using CommunityPortalREST.Models.Repositories;
using CommunityPortalREST.Models.ViewModels.Category;

namespace CommunityPortalREST.Models.Services
{
    public class CategoryService : ICategoryService
    {
        private CategoryRepository Repository { get; }

        public CategoryService(CategoryRepository repository)
        {
            Repository = repository;
        }

        public List<Category> GetAll()
        {
            return Repository.Read();
        }

        public Category GetById(int id)
        {
            return Repository.Read(id);
        }

        public Category Add(CreateCategoryViewModel entry)
        {
            Category item = new Category()
            {
                Title = entry.Title
            };

            return Repository.Create(item);
        }

        public bool Edit(int id, CreateCategoryViewModel entry)
        {
            Category item = new Category()
            {
                Id = id,
                Title = entry.Title
            };

            return Repository.Update(item);
        }

        public bool Remove(int id)
        {
            Category item = GetById(id);
            return item != null && Repository.Delete(item);
        }

        public Category MakeAsMenu(int id)
        {
            Category entry = GetById(id);

            if (entry != null)
            {
                entry.Menu = true;

                Repository.Update(entry);
            }

            return entry;
        }

        public Category ReleaseFromMenu(int id)
        {
            Category entry = GetById(id);

            if (entry != null)
            {
                entry.Menu = false;

                Repository.Update(entry);
            }

            return entry;
        }

        public List<Category> Menus()
        {
            return GetAll()
                .Where(item => item.Menu)
                .ToList();
        }
    }
}
