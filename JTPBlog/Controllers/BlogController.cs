using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using ViewModelServiceInterfaces;
using JTPBlog.Models;
using System.Collections.Generic;
using JTPBlog.Models.PageViewModels;
using ViewModels;
using JTPBlog.Services.Interfaces;
using System.Threading.Tasks;

namespace JTPBlog.Controllers
{
    public class BlogController : Controller
    {
        IBlogPostVMService blogPostVMService;
        IS3Service s3Service;
        public BlogController(IBlogPostVMService blogPostVMService, IS3Service s3Service)
        {
            this.blogPostVMService = blogPostVMService;
            this.s3Service = s3Service;
        }


        public async Task<ActionResult> BlogPost(int blogID)
        {
            var blog = blogPostVMService.GetByID(blogID);
            ViewBag.Message = "Post";
            
            return View(blog);
        }
        public ActionResult BlogIndexv2(string sortOrder, string currentFilter, string searchString, int? page, string category)
        {
            IQueryable<BlogPostVM> blogs = blogPostVMService.GetAll().AsQueryable<BlogPostVM>();
            if (!String.IsNullOrEmpty(category))
            {
                blogs = blogs.Where(s => s.Category.Contains(category));
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                blogs = blogs.Where(s => s.Title.Contains(searchString)
                                       || s.Description.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "title_desc":
                    blogs = blogs.OrderByDescending(b => b.Title);
                    break;
                case "Date":
                    blogs = blogs.OrderBy(b => b.DateCreated);
                    break;
                case "date_desc":
                    blogs = blogs.OrderByDescending(b => b.DateCreated);
                    break;
                default:
                    blogs = blogs.OrderByDescending(b => b.DateCreated);
                    break;
            }

            int pageSize = 4;
            int pageNumber = (page ?? 1);
            return View(PaginatedList<BlogPostVM>.CreateAsync(blogs, page ?? 1, pageSize));
        }
        public ActionResult BlogIndex(string sortOrder, string currentFilter, string searchString, int? page, string category)
        {
            ViewData["TitleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.CurrentSort = sortOrder;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            ViewBag.CurrentCategory = category;

            IEnumerable<BlogPostVM> blogs = blogPostVMService.GetAll();
            if (!String.IsNullOrEmpty(category))
            {
                blogs = blogs.Where(s => !string.IsNullOrEmpty(s.Category) && s.Category.Contains(category));
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                blogs = blogs.Where(s => s.Title.Contains(searchString)
                                       || s.Description.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "title_desc":
                    blogs = blogs.OrderByDescending(b => b.Title);
                    break;
                case "Date":
                    blogs = blogs.OrderBy(b => b.DateCreated);
                    break;
                case "date_desc":
                    blogs = blogs.OrderByDescending(b => b.DateCreated);
                    break;
                default:
                    blogs = blogs.OrderByDescending(b => b.DateCreated);
                    break;
            }

            int pageSize = 4;
            int pageNumber = (page ?? 1);
            return View(PaginatedList<BlogPostVM>.CreateAsync(blogs, page ?? 1, pageSize));
        }
        public ActionResult Archive()
        {
            ViewBag.Message = "Archive - See all posts";

            IEnumerable<BlogPostVM> blogs = blogPostVMService.GetAll();
            return View(blogs);
        }
        public ActionResult CreateBlogPost()
        {
            var cats = blogPostVMService.GetAllCategories();
            var tags = blogPostVMService.GetTagsWithCount();
            CreateBlogPageViewModel cbpvm = new CreateBlogPageViewModel()
            {
                Categories = cats,
                Tags = tags
            };
            ViewBag.Message = "Create posts";

            return View(cbpvm);
        }
    }
}