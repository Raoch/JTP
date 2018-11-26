using JTPBlog.Models;
using JTPBlog.Services.Interfaces;
using JTPBlog.Services.Interfaces.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels;
using ViewModelServiceInterfaces;

namespace JTPBlog.Controllers.Api
{
    public class BlogApiController : Controller
    {
        IBlogPostVMService blogPostVMService;
        IS3Service s3Service;

        public BlogApiController(IBlogPostVMService blogPostVMService, IS3Service s3Service)
        {
            this.blogPostVMService = blogPostVMService;
            this.s3Service = s3Service;
        }


        [HttpPost]
        public async Task<int> PostBlog([FromBody]PostRequest bpToAdd)
        {
            S3DownloadObject s3TestObj;
            S3ObjectResponse s3TestObjs;
            S3ObjectResponse s3TestObjfilter;
            List<string> filters = new List<string>() { "test/testBucketItem.pdf", "test/testBucketItem2.pdf", "test/testBucketItem3.pdf", "test/testBucketItem4.pdf", "test/testBucketItem5.pdf" };

            S3ObjectResponse s3TestMultiGet;
            try {
                //var s3TestObj = await s3Service.DownloadS3Objects();
                s3TestObj = await s3Service.DownloadS3ObjectByName("jtp-blog", "test/testBucketItem.pdf");

                s3TestObjs = await s3Service.DownloadS3ObjectsByPrefix("jtp-blog", "test/testBucketItem");

                s3TestObjfilter = await s3Service.DownloadS3ObjectsWithFilter("jtp-blog", "test/testBucketItem", filters);

                s3TestMultiGet = await s3Service.DownloadS3ObjectsByListOfNames("jtp-blog", filters);
            }
            catch (Exception e) {
                    var res = e;
            }
            
            return 1;
        }
        public async Task<BlogPostVM> GetBlogByID(int id)
        {

            return blogPostVMService.GetByID(id);
        }
        public PaginationViewModel<BlogPostVM> GetBlogByCategoryID(int id)
        {
            return blogPostVMService.GetByCategory(id);
        }
        public IEnumerable<BlogCategory> GetBlogCategorys()
        {
            return blogPostVMService.GetAllCategories();
        }


    }
}
