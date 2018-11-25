
using JTPBlog.Models;
using System.Collections.Generic;

namespace JTPBlog.Service.Interfaces
{
    public interface ITagService
    {
        IEnumerable<Tag> GetAllTags();
        Tag GetByID(int id);
        IEnumerable<Tag> GetTagsWithCount();
        int Count();
    }
}
