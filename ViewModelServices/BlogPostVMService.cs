using JTPBlog.Models;
using JTPBlog.Service.Interfaces;
using System;
using System.Collections.Generic;
using ViewModels;
using ViewModelServiceInterfaces;
using System.Linq; 

namespace ViewModelServices
{
    public class BlogPostVMService : IBlogPostVMService
    {
        private readonly IBlogPostService blogService;
        private readonly ITagService tagService;
        private readonly IBlogCategoryService blogCategoryService;
        public BlogPostVMService(IBlogPostService blogService, IBlogCategoryService blogCategoryService, ITagService tagService)
        {
            this.blogService = blogService;
            this.blogCategoryService = blogCategoryService;
            this.tagService = tagService;
        }

        public int CreateBlog(BlogPostVM bpToAdd)
        {

            int blogPostID = blogService.CreateBlog(bpToAdd.ToModel());
            return blogPostID;
        }

        public IEnumerable<BlogPostVM> GetAll()
        {
            var blogs = blogService.GetAll().Select(x => new BlogPostVM(x));
            return blogs;
        }

        public BlogPostVM GetByID(int id)
        {
            var blog = blogService.GetByID(id);
            return new BlogPostVM(blog);
        }
        public PaginationViewModel<BlogPostVM> GetPaginatedList()
        {
            var blogs = blogService.GetPaginatedList();
            int total = blogService.Count();
            return new PaginationViewModel<BlogPostVM>() { NumberOfRows = total, Content = blogs.Select(x => new BlogPostVM(x)), };
        }
        public PaginationViewModel<BlogPostVM> GetByCategory(int id)
        {
            var blogs = blogService.GetPaginatedList();
            int total = blogService.Count();
            return new PaginationViewModel<BlogPostVM>() { NumberOfRows = total, Content = blogs.Select(x => new BlogPostVM(x)), };
        }
        public IEnumerable<BlogCategory> GetAllCategories()
        {
            var blogCategories = blogCategoryService.GetAll();
            return blogCategories;
        }
        public IEnumerable<Tag> GetTagsWithCount()
        {
            return tagService.GetTagsWithCount();
        }
    }
}
