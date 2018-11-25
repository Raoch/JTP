
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace JTPBlog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls("http://jtpblog:80;http://localhost:80;https://hostname:5002")
                .Build();
    }
}
