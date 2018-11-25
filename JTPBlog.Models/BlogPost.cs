using System;
using System.Collections.Generic;

namespace JTPBlog.Models
{
    public class BlogPost
    {
        public int BlogPostID { get; set; }
        public string Title{ get; set; }
        public string ContentText { get; set; }
        public string ContentHtml { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public bool IsPublished { get; set; }
        public int AuthorID { get; set; }
        public int CategoryID { get; set; }
        public string Category { get; set; }
        [DBIgnore]
        public DateTime DateCreated { get; set; }
        [DBIgnore]
        public DateTime DateModified { get; set; }
        [DBIgnore]
        public Tag[] Tags { get; set; }
    }
}
