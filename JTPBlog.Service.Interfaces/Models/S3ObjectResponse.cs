using System.Collections.Generic;

namespace JTPBlog.Services.Interfaces.Models
{
    public class S3ObjectResponse
    {
        public string BucketName { get; set; }
        public string S3Location { get; set; }
        public string Prefix { get; set; }
        public List<S3DownloadObject> S3DownloadObjects { get; set; }
    }

    public class S3DownloadObject
    {
        public string FileName { get; set; }
        public string FileBaseString { get; set; }
    }
}
