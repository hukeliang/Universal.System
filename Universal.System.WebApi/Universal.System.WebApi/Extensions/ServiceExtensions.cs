using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Universal.System.WebApi.Extensions
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// 数据库链接对象注入 IDbConnection
        /// </summary>
        /// <param name="services"></param>
        /// <param name="master"></param>
        /// <returns></returns>
        public static IServiceCollection AddDbConnectionMaster(this IServiceCollection services, string master)
        {
            //同一个线程上下文中只创建一个链接对象 
            services.AddScoped(typeof(IDbConnection), provider =>
            {
                 SqlConnection sqlConnection = new SqlConnection(master);

                 Console.Out.WriteLine($"{nameof(sqlConnection)}被构建");
                 return sqlConnection;
            });

            return services;
        }
    }
}
