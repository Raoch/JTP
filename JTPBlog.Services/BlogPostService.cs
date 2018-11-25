using System.Collections.Generic;
using System.Linq;
using Data.Interfaces;
using JTPBlog.Models;
using JTPBlog.Service.Interfaces;

namespace JTPBlog.Services
{
    public class BlogPostService : IBlogPostService
    {
        IRepository<BlogPost> blogPostRepository;
        IRepository<Tag> tagRepository;
        IRepository<MapBlogPost_Tag> mbptRepository;

        public BlogPostService(IRepository<BlogPost> blogPostRepository, IRepository<Tag> tagRepository, IRepository<MapBlogPost_Tag> mbptRepository)
        {
            this.blogPostRepository = blogPostRepository;
            this.tagRepository = tagRepository;
            this.mbptRepository = mbptRepository;
        }
        public IEnumerable<BlogPost> GetPaginatedList()
        {
            //return blogPostRepository.GetPaginatedList();
            return null;
        }

        public IEnumerable<BlogPost> GetAll()
        {
            return blogPostRepository.GetAll().AsQueryable().OrderByDescending(x => x.DateCreated);
        }

        public BlogPost GetByID(int id)
        {
            BlogPost blogPost = blogPostRepository.Get("BlogPostID", id);
            return blogPost;
        }

        public int Count()
        {
            return blogPostRepository.Count("IsActive = True", null);
        }

        public int CreateBlog(BlogPost bpToAdd)
        {
            List<MapBlogPost_Tag> maps = new List<MapBlogPost_Tag>();
            // create blog post
            var newBlogID = blogPostRepository.Insert<BlogPost>(bpToAdd);

            for (int i = 0; i < bpToAdd.Tags.Length; i++)
            {
                var mbpt = new MapBlogPost_Tag()
                {
                    BlogPostID = newBlogID
                };
                //check if tag exists
                var tag = tagRepository.Get("Name", bpToAdd.Tags[i].Name);
                // if tag exists then use its tag ID else create new tag and use that ID
                mbpt.TagID = tag != null ? tag.ID : tagRepository.Insert<Tag>(new Tag() {Name = bpToAdd.Tags[i].Name });

                // create MapBlogPostTag 
                var mbptResult = mbptRepository.Insert<MapBlogPost_Tag>(mbpt);
            };
            return newBlogID;
        }
    }
}
