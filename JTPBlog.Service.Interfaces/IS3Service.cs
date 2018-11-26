using JTPBlog.Services.Interfaces.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JTPBlog.Services.Interfaces
{
    public interface IS3Service
    {
        Task<S3DownloadObject> DownloadS3ObjectByName(string bucketName, string key);
        Task<S3ObjectResponse> DownloadS3ObjectsByPrefix(string bucketName, string prefix);
        Task<S3ObjectResponse> DownloadS3ObjectsByListOfNames(string bucketName, IEnumerable<string> prefix);
        Task<S3ObjectResponse> DownloadS3ObjectsWithFilter(string bucketName, string prefix, List<string> filters);
    }
}
