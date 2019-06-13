using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Universal.System.WebApi
{
    public class Program
    {
        /// <summary>
        /// Application Run
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
