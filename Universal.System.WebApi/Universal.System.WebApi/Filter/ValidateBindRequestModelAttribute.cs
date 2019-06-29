using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Universal.System.Common;
using Universal.System.WebApi.Extensions;

namespace Universal.System.WebApi.Filter
{
    /// <summary>
    /// 验证模型
    /// </summary>
    /// <param name="context"></param>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class ValidateBindRequestModelAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            //POST PUT 方法进行模型校验
            string method = context.HttpContext.Request.Method;

            if ((method == "POST" || method == "PUT") && !context.ModelState.IsValid)
            {
                //BindAttribute特性对JSON数据无效
                IList<ParameterDescriptor> parameterDescriptors = context.ActionDescriptor.Parameters;
                //获取BindAttribute
                ParameterDescriptor parameterDescriptor = parameterDescriptors.Where(item => item.BindingInfo.PropertyFilterProvider is BindAttribute).SingleOrDefault();

                BindAttribute bindAttribute = parameterDescriptor.BindingInfo.PropertyFilterProvider as BindAttribute;

                string message = context.ModelState.ToErrorMessageByBindAttribute(bindAttribute);

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
