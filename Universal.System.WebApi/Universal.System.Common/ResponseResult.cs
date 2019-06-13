using Microsoft.AspNetCore.Http;

namespace Universal.System.Common
{
    public sealed class ResponseResult
    {
        /// <summary>
        /// 响应代码
        /// </summary>
        public int Code { get; private set; }
        /// <summary>
        /// 响应消息内容
        /// </summary>
        public dynamic Response { get; private set; }


        /// <summary>
        /// 设置响应状态为成功
        /// </summary>
        /// <param name="message"></param>
        public ResponseResult Success(string message = "请求成功")
        {
            Code = StatusCodes.Status200OK;
            Response = message;
            return this;
        }
        /// <summary>
        /// 设置响应状态为失败
        /// </summary>
        /// <param name="message"></param>
        public ResponseResult Failed(string message = "失败")
        {
            Code = StatusCodes.Status403Forbidden;
            Response = message;
            return this;
        }

        /// <summary>
        /// 设置响应状态为错误
        /// </summary>
        /// <param name="message"></param>
        public ResponseResult Error(string message = "错误")
        {
            Code = StatusCodes.Status500InternalServerError;
            Response = message;
            return this;
        }

        /// <summary>
        /// 设置响应状态为未知资源
        /// </summary>
        /// <param name="message"></param>
        public ResponseResult NotFound(string message = "未知资源")
        {
            Code = StatusCodes.Status404NotFound;
            Response = message;
            return this;
        }

        /// <summary>
        /// 设置响应状态为无权限
        /// </summary>
        /// <param name="message"></param>
        public ResponseResult NoPermission(string message = "无权访问")
        {
            Code = StatusCodes.Status401Unauthorized;
            Response = message;
            return this;
        }

        /// <summary>
        /// 设置响应数据
        /// </summary>
        /// <param name="data"></param>
        public ResponseResult ResponseData(dynamic value)
        {
            Code = StatusCodes.Status200OK;
            Response = value;
            return this;
        }
    }
}
