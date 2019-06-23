using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using Universal.System.Common;
using Universal.System.WebApi.Extensions;

namespace Universal.System.WebApi.Filter
{
    /// <summary>
    /// 验证模型
    /// </summary>
    /// <param name="context"></param>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class ValidateModelAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            //POST PUT 方法进行模型校验
            string method = context.HttpContext.Request.Method;

            if ((method == "POST" || method == "PUT") && !context.ModelState.IsValid)
            {
                string message = context.ModelState.ToErrorMessage();
                if (message != string.Empty)
                {
                    ResponseResult responseResult = CommonFactory.CreateResponseResult;
                    context.Result = new JsonResult(responseResult.Failed(message));
                    return;
                }
            }
        }

        public virtual void OnActionExecuted(ActionExecutedContext context) { }

    }
}
