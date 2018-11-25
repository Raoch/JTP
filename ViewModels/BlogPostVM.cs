using JTPBlog.Models;
using System;
using System.Linq;

namespace ViewModels
{
    public class BlogPostVM
    {
        public int BlogPostID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ContentText { get; set; }
        public string ContentHtml { get; set; }
        public string Category { get; set; }
        public int CategoryID { get; set; }
        public int AuthorID { get; set; }
        public string ImageURL { get; set; }
        public bool IsPublished { get; set; }
        public DateTime DateModified { get; set; }
        public DateTime DateCreated { get; set; }
        public Tag[] Tags { get; set; }

        public BlogPostVM()
        {

        }
        public BlogPostVM(BlogPost blogPost)
        {
            this.Title = blogPost.Title;
            this.AuthorID = blogPost.AuthorID;
            this.BlogPostID = blogPost.BlogPostID;
            this.CategoryID = blogPost.CategoryID;
            this.ContentHtml = blogPost.ContentHtml;
            this.ContentText = blogPost.ContentText;
            this.Description = blogPost.Description;
            this.ImageURL = blogPost.ImageURL;
            this.IsPublished = blogPost.IsPublished;
            this.Category = blogPost.Category;
            this.Tags = blogPost.Tags;
        }

        public BlogPost ToModel()
        {
            BlogPost bpModel = new BlogPost()
            {
                AuthorID = this.AuthorID,
                BlogPostID = this.BlogPostID,
                Title = this.Title,
                IsPublished = this.IsPublished,
                ImageURL = this.ImageURL,
                CategoryID = this.CategoryID,
                ContentText = this.ContentText,
                ContentHtml = this.ContentHtml,
                DateCreated = this.DateCreated,
                DateModified = this.DateCreated,
                Description = this.Description,
                Category = this.Category,
                Tags = this.Tags
            };
            return bpModel;
        }
    }
}
