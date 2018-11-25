
using JTPBlog.Models;
using System.Collections.Generic;

namespace JTPBlog.Service.Interfaces
{
    public interface IBlogCategoryService
    {
        IEnumerable<BlogCategory> GetAll();
        BlogCategory GetByID(int id);
        IEnumerable<BlogCategory> GetPaginatedList();
        int Count();
    }
}
