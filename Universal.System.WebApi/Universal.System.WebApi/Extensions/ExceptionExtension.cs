using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Universal.System.WebApi.Middleware;

namespace Universal.System.WebApi.Extensions
{
    /// <summary>
    /// 全局异常拓展
    /// </summary>
    public static class ExceptionExtension
    {
        /// <summary>
        /// 使用500错误全局捕获
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseInternalServerError(this IApplicationBuilder buider)
        {
            buider.UseMiddleware<InternalServerErrorMiddleware>();

            return buider;
        }
    }
}
