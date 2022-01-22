using System;
using System.Collections.Generic;
using System.Linq;
using CommunityPortalREST.Models.Domains;
using CommunityPortalREST.Models.Repositories;
using CommunityPortalREST.Models.ViewModels.Post;

namespace CommunityPortalREST.Models.Services
{
    public class PostService : IPostService
    {
        private PostRepository Repository { get; }

        public PostService(PostRepository repository)
        {
            Repository = repository;
        }

        public List<Post> GetAll()
        {
            return Repository.Read();
        }

        public Post GetById(int id)
        {
            return Repository.Read(id);
        }

        public Post Add(CreatePostViewModel entry)
        {
            Post item = new Post()
            {
                Title = entry.Title,
                Thumbnail = entry.Thumbnail,
                Content = entry.Content,
                Created = TimeSpan.FromSeconds(0)
            };

            try
            {
                item = Repository.Create(item);

                foreach (int category in entry.Categories)
                {
                    TrackCategory(item.Id, category);
                }
            }
            catch (Exception e)
            {
                item = null;
            }

            return item;
        }

        public bool Edit(int id, CreatePostViewModel entry)
        {
            bool result = false;

            Post post = GetById(id);

            if (post != null)
            {
                foreach (int category in post.Categories
                    .Select(item => item.CategoryId)
                    .ToList())
                {
                    SubTrackCategory(id, category);
                }

                foreach (int category in entry.Categories)
                {
                    TrackCategory(id, category);
                }

                post.Title = entry.Title;
                post.Thumbnail = entry.Thumbnail;
                post.Content = entry.Content;
                post.Created = entry.Created;

                result = Repository.Update(post);
            }

            return result;
        }

        public bool Remove(int id)
        {
            Post entry = GetById(id);

            if (entry != null && entry.Categories.Any())
            {
                entry.Categories.Clear();
                Repository.Update(entry);
            }

            return entry != null && Repository.Delete(entry);
        }

        public bool TrackCategory(int post, int category)
        {
            bool result = false;

            Post entry = GetById(post);

            if (entry != null)
            {
                if (entry.Categories == null)
                {
                    entry.Categories = new List<CategoryPost>();
                }

                try
                {
                    entry.Categories.Add(new CategoryPost()
                    {
                        PostId = post,
                        CategoryId = category
                    });

                    result = Repository.Update(entry);
                }
                catch (Exception e)
                {
                    result = false;
                }

            }

            return result;
        }

        public bool SubTrackCategory(int post, int category)
        {
            bool result = false;

            Post entry = GetById(post);

            if (
                entry != null &&
                entry.Categories.Any(item => item.CategoryId == category)
                )
            {
                CategoryPost item = entry.Categories
                    .Single(item => item.CategoryId == category);

                entry.Categories.Remove(item);

                result = Repository.Update(entry);

            }

            return result;
        }
    }
}
