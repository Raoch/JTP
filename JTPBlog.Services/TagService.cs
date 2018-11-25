using System.Collections.Generic;
using Data.Interfaces;
using JTPBlog.Models;
using JTPBlog.Service.Interfaces;

namespace JTPBlog.Services
{
    public class TagService : ITagService
    {
        IRepository<Tag> tagRepository;
        IRepository<MapBlogPost_Tag> mapBlogPostTagRepository;

        public TagService(IRepository<Tag> tagRepository, IRepository<MapBlogPost_Tag> mapBlogPostTagRepository)
        {
            this.tagRepository = tagRepository;
            this.mapBlogPostTagRepository = mapBlogPostTagRepository;
        }

        public IEnumerable<Tag> GetAllTags()
        {
            return tagRepository.GetAll();
        }

        public Tag GetByID(int id)
        {
            Tag blogPost = tagRepository.Get("BlogPostID", id);
            return blogPost;
        }
        public IEnumerable<Tag> GetTagsWithCount()
        {
            return tagRepository.ExecuteSql("SELECT blogapi.tag.ID, blogapi.tag.Name, COUNT(blogapi.Tag.ID) as Count FROM blogapi.tag LEFT JOIN mapblogpost_tag ON blogapi.tag.ID = mapblogpost_tag.TagID GROUP BY blogapi.tag.ID, blogapi.tag.Name;");
        }

        public int Count()
        {
            return tagRepository.Count("IsActive = True", null);
        }
    }
}
