using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JTPBlog.Models.PageViewModels
{
    public class CreateBlogPageViewModel
    {
        public IEnumerable<BlogCategory> Categories { get; set; }
        public IEnumerable<Tag> Tags { get; set; }

    }
}
