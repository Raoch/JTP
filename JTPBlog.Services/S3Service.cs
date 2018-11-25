using Amazon.S3;
using Amazon;
using Amazon.S3.Model;
using System;
using System.Threading.Tasks;
using System.IO;
using JTPBlog.Services.Interfaces;
using System.Collections.Generic;
using JTPBlog.Services.Interfaces.Models;
using System.Linq;
using System.Linq.Expressions;

namespace JTPBlog.Services
{
    public class S3Service : IS3Service
    {
        private IAmazonS3 client;

        public S3Service(IAmazonS3 client)
        {
            this.client = client;
        }

        public async Task<S3ObjectResponse> DownloadS3ObjectsWithQuery(string bucketName, string prefix, Expression<Func<S3Object, bool>> expression)
        {
            S3ObjectResponse s3ObjectResponse = new S3ObjectResponse();
            s3ObjectResponse.S3DownloadObjects = new List<S3DownloadObject>();

            ListObjectsResponse objectsResponse = await this.GetListOfS3Objects(bucketName, prefix);

            s3ObjectResponse.BucketName = objectsResponse.Name;
            s3ObjectResponse.Prefix = objectsResponse.Prefix;

            IEnumerable<S3Object> s3 = objectsResponse.S3Objects.AsQueryable().Where(expression).ToList();

            foreach (S3Object s3Object in objectsResponse.S3Objects)
            {
                string base64 = await this.DownloadS3Object(s3Object.BucketName, s3Object.Key);

                s3ObjectResponse.S3DownloadObjects.Add(new S3DownloadObject()
                {
                    FileName = s3Object.Key.Split('/').Last(),
                    FileBaseString = base64
                });
            }

            return s3ObjectResponse;
        }

        public async Task<S3ObjectResponse> DownloadS3ObjectsWithFilter(string bucketName, string prefix, List<string> filters)
        {
            S3ObjectResponse s3ObjectResponse = new S3ObjectResponse();
            s3ObjectResponse.S3DownloadObjects = new List<S3DownloadObject>();

            ListObjectsResponse objectsResponse = await this.GetListOfS3Objects(bucketName, prefix);

            s3ObjectResponse.BucketName = objectsResponse.Name;
            s3ObjectResponse.Prefix = objectsResponse.Prefix;

            List<S3Object> objects = objectsResponse.S3Objects.Where(s => filters.Contains(s.Key)).ToList();

            objectsResponse.S3Objects = objectsResponse.S3Objects.Where(s => filters.Any(f => f.EndsWith(s.Key))).ToList();
            //objectsResponse.S3Objects = objectsResponse.S3Objects.Select(x => x).Where(x => filters.Contains(x.Key)).ToList();

            foreach (S3Object s3Object in objectsResponse.S3Objects)
            {
                string base64 = await this.DownloadS3Object(s3Object.BucketName, s3Object.Key);

                s3ObjectResponse.S3DownloadObjects.Add(new S3DownloadObject()
                {
                    FileName = s3Object.Key.Split('/').Last(),
                    FileBaseString = base64
                });
            }

            return s3ObjectResponse;
        }

        public async Task<S3ObjectResponse> DownloadS3Objects(string bucketName, string prefix)
        {
            var s3ObjectResponse = new S3ObjectResponse();
            s3ObjectResponse.S3DownloadObjects = new List<S3DownloadObject>();

            var objectsResponse = await this.GetListOfS3Objects(bucketName, prefix);

            s3ObjectResponse.BucketName = objectsResponse.Name;
            s3ObjectResponse.Prefix = objectsResponse.Prefix;

            foreach (var s3Object in objectsResponse.S3Objects)
            {
                var base64 = await this.DownloadS3Object(s3Object.BucketName, s3Object.Key);

                s3ObjectResponse.S3DownloadObjects.Add(new S3DownloadObject()
                {
                    FileName = s3Object.Key.Split('/').Last(),
                    FileBaseString = base64
                });
            }

            return s3ObjectResponse;
        }

        public async Task<string> DownloadS3Object(string bucketName, string key)
        {
            string base64 = "";
            try
            {
                var getObjectRequest = new GetObjectRequest()
                {
                    BucketName = bucketName,
                    Key = key
                };

                using (var response = await client.GetObjectAsync(getObjectRequest))
                using (var responseStream = response.ResponseStream)
                using (MemoryStream mem = new MemoryStream())
                {
                    var buffer = new byte[4096];
                    int bytesRead;
                    while ((bytesRead = responseStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        mem.Write(buffer, 0, bytesRead);
                    }

                    base64 = Convert.ToBase64String(mem.ToArray());
                }
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine("Error encountered ***. Message:'{0}' when writing an object", e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unknown encountered on server. Message:'{0}' when writing an object", e.Message);
            }

            return base64; //TODO: dont return null
        }

        private async Task<ListObjectsResponse> GetListOfS3Objects(string bucketName, string prefix)
        {
            return await client.ListObjectsAsync(new ListObjectsRequest
            {
                BucketName = bucketName, // Main folder
                Prefix = prefix // Sub folders
            });
        }
    }
}
