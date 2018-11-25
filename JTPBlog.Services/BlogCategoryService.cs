using System.Collections.Generic;
using Data.Interfaces;
using JTPBlog.Models;
using JTPBlog.Service.Interfaces;

namespace JTPBlog.Services
{
    public class BlogCategoryService : IBlogCategoryService
    {
        IRepository<BlogCategory> blogCategoryRepository;

        public BlogCategoryService(IRepository<BlogCategory> blogCategoryRepository)
        {
            this.blogCategoryRepository = blogCategoryRepository;
        }
        public IEnumerable<BlogCategory> GetPaginatedList()
        {
            //return blogPostRepository.GetPaginatedList();
            return null;
        }

        public IEnumerable<BlogCategory> GetAll()
        {
            return blogCategoryRepository.GetAll();
        }

        public BlogCategory GetByID(int id)
        {
            BlogCategory blogCategory = blogCategoryRepository.Get("ID", id);
            return blogCategory;
        }

        public int Count()
        {
            return blogCategoryRepository.Count("Name IS NOT Null", null);
        }
    }
}
