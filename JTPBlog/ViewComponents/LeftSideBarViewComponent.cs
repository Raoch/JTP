using JTPBlog.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModelServiceInterfaces;

namespace JTPBlog.ViewComponents
{
    public class LeftSidebarViewComponent : ViewComponent
    {
        private readonly IBlogPostVMService blogPostVMService;

        public LeftSidebarViewComponent(IBlogPostVMService blogPostVMService)
        {
            this.blogPostVMService = blogPostVMService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = GetCategories();
            return View(items);
        }
        private IEnumerable<BlogCategory> GetCategories()
        {
            return blogPostVMService.GetAllCategories();
        }
    }
}
