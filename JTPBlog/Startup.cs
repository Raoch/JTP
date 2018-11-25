using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.S3;
using Data;
using Data.Interfaces;
using JTPBlog.Models;
using JTPBlog.Service.Interfaces;
using JTPBlog.Services;
using JTPBlog.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ViewModelServiceInterfaces;
using ViewModelServices;

namespace JTPBlog
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            string connectionString = "server=localhost;database=blogapi;User ID=Blog_ReadWrite;password=test123;SslMode=none;";
            services.AddScoped<IUnitOfWork>(e => new UnitOfWork(connectionString));
            services.AddScoped<IRepository<BlogPost>, Repository<BlogPost>>();
            services.AddScoped<IRepository<BlogCategory>, Repository<BlogCategory>>();
            services.AddScoped<IRepository<Tag>, Repository<Tag>>();
            services.AddScoped<IRepository<MapBlogPost_Tag>, Repository<MapBlogPost_Tag>>();

            services.AddTransient<IBlogPostService, BlogPostService>();
            services.AddTransient<IBlogCategoryService, BlogCategoryService>();
            services.AddTransient<ITagService, TagService>();
            services.AddScoped<IS3Service, S3Service>();

            services.AddDefaultAWSOptions(Configuration.GetAWSOptions());
            services.AddAWSService<IAmazonS3>();

            services.AddTransient<IBlogPostVMService, BlogPostVMService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
