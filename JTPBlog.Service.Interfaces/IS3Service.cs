using Amazon.S3.Model;
using JTPBlog.Services.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace JTPBlog.Services.Interfaces
{
    public interface IS3Service
    {
        Task<string> DownloadS3Object(string bucketName, string key);
        Task<S3ObjectResponse> DownloadS3Objects(string bucketName, string prefix);
        Task<S3ObjectResponse> DownloadS3ObjectsWithFilter(string bucketName, string prefix, List<string> filters);
        Task<S3ObjectResponse> DownloadS3ObjectsWithQuery(string bucketName, string prefix, Expression<Func<S3Object, bool>> expression);
    }
}
