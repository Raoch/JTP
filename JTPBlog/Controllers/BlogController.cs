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
using System.Text;
using System.IO;
using System.IO.Compression;

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
        public async virtual Task<FileResult> GetInvoice()
        {
            var result = await s3Service.DownloadS3ObjectByName("jtp-blog", "test/testBucketItem.pdf");

            return File(Convert.FromBase64String(result.FileBaseString), "application/pdf", result.FileName);
        }
        public async Task<ActionResult> GetInvoices()
        {
            List<string> list = new List<string>() { "test/testBucketItem.pdf", "test/testBucketItem2.pdf", "test/testBucketItem3.pdf" };
            var result = await s3Service.DownloadS3ObjectsByListOfNames("jtp-blog", list);
            // get the source files
            List<FileModel> sourceFiles = new List<FileModel>();
            // ...

            // the output bytes of the zip
            byte[] fileBytes = null;

            // create a working memory stream
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // create a zip
                using (ZipArchive zip = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    // interate through the source files
                    foreach (var f in  result.S3DownloadObjects)
                    {
                        // add the item name to the zip
                        ZipArchiveEntry zipItem = zip.CreateEntry(f.FileName + "." + "pdf");
                        // add the item bytes to the zip entry by opening the original file and copying the bytes 
                        using (MemoryStream originalFileMemoryStream = new MemoryStream(Convert.FromBase64String(f.FileBaseString)))
                        {
                            using (Stream entryStream = zipItem.Open())
                            {
                                originalFileMemoryStream.CopyTo(entryStream);
                            }
                        }
                    }
                }
                fileBytes = memoryStream.ToArray();
            }

            // download the constructed zip
            Response.Headers.Add("Content-Disposition", "attachment; filename=download.zip");
            return File(fileBytes, "application/zip");
        }
        public async virtual Task<FileResult> GetInvoicesOld()
        {
            List<string> list = new List<string>() { "test/testBucketItem.pdf", "test/testBucketItem2.pdf", "test/testBucketItem3.pdf" };
            var result = await s3Service.DownloadS3ObjectsByListOfNames("jtp-blog", list);
            MemoryStream outputStream = new MemoryStream();
            outputStream.Seek(0, SeekOrigin.Begin);
            //ZipFile zip = new ZipFile()
            //foreach (var obj in result.S3DownloadObjects)
            //{
            //    var byteArray = Convert.FromBase64String(obj.FileBaseString);
            //    using (var stream = new MemoryStream(file))
            //    {
            //        zip.AddEntry("invoice" + id + ".pdf", stream);
            //    }
            //}
            return File(Convert.FromBase64String(result.S3DownloadObjects.FirstOrDefault().FileBaseString), "application/pdf", result.S3DownloadObjects.FirstOrDefault().FileName);
        }
    }
}