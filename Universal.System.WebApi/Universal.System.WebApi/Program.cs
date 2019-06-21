using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

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
            /**
             * Git for wnedows 国内镜像站
             * https://github.com/waylau/git-for-win
             */

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args).UseStartup<Startup>().ConfigureLogging(logging =>
            {
                //配置Nlog替换默认日志记录工具

                //清空所有日志输出方式
                logging.ClearProviders();

                //添加控制台输出 --
                logging.AddConsole();

            }).UseNLog();
        }
    }
}
