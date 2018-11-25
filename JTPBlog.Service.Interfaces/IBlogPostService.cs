
using JTPBlog.Models;
using System.Collections.Generic;

namespace JTPBlog.Service.Interfaces
{
    public interface IBlogPostService
    {
        IEnumerable<BlogPost> GetAll();
        BlogPost GetByID(int id);
        IEnumerable<BlogPost> GetPaginatedList();
        int Count();
        int CreateBlog(BlogPost bpToAdd);
    }
}
