using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Universal.System.Common;

namespace Universal.System.WebApi.Middleware
{
    /// <summary>
    /// 全局异常捕获中间件
    /// </summary>
    public class InternalServerErrorMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public InternalServerErrorMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            //PS：在请求管道中不是单例不能使用构造函数注入 
            _next = next;
            _logger = loggerFactory.CreateLogger<InternalServerErrorMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                HttpResponse response = context.Response;

                response.Clear();
                response.StatusCode = StatusCodes.Status500InternalServerError;   //发生未捕获的异常，手动设置状态码

                if (response.HasStarted)
                {
                    _logger.LogWarning("响应已经启动，错误中间件将不会被执行");
                    throw;
                }

                ResponseResult responseResult = CommonFactory.CreateResponseResult;

                string result = JsonConvert.SerializeObject(responseResult.Error());

                await response.WriteAsync(result);

                _logger.LogError(ex, "执行请求时发生未处理的异常");

            }
        }
    }
}
