using JTPBlog.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModelServiceInterfaces;

namespace JTPBlog.ViewComponents
{
    public class RightSidebarViewComponent : ViewComponent
    {
        private readonly IBlogPostVMService blogPostVMService;

        public RightSidebarViewComponent(IBlogPostVMService blogPostVMService)
        {
            this.blogPostVMService = blogPostVMService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = GetTags();
            return View(items);
        }
        private IEnumerable<Tag> GetTags()
        {
            return blogPostVMService.GetTagsWithCount();
        }
    }
}
