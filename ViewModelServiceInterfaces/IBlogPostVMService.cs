using JTPBlog.Models;
using System.Collections.Generic;
using ViewModels;

namespace ViewModelServiceInterfaces
{
    public interface IBlogPostVMService
    {
        IEnumerable<BlogPostVM> GetAll();
        BlogPostVM GetByID(int id);
        PaginationViewModel<BlogPostVM> GetPaginatedList();
        PaginationViewModel<BlogPostVM> GetByCategory(int id);
        IEnumerable<BlogCategory> GetAllCategories();
        IEnumerable<Tag> GetTagsWithCount();
        int CreateBlog(BlogPostVM bpToAdd);
    }
}
