using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System;
using System.Reflection;
using Universal.System.Common;
using Universal.System.Service.Interface;

namespace Universal.System.WebApi.Filter
{
    /// <summary>
    /// 验证Token
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class ValidateTokenAttribute : Attribute, IAuthorizationFilter, IOrderedFilter
    {
        public int Order => 0;

        private readonly IAuthService _authService;

        public ValidateTokenAttribute(IAuthService authService)
        {
            _authService = authService;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            ControllerActionDescriptor controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;

            AllowAnonymousAttribute allowAnonymous = controllerActionDescriptor.MethodInfo.GetCustomAttribute(typeof(AllowAnonymousAttribute), false) as AllowAnonymousAttribute;

            if (allowAnonymous  == null)
            {
                const string prefix = "Bearer ";

                bool result = context.HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues authorization);

                ResponseResult responseResult = CommonFactory.CreateResponseResult;

                JsonResult jsonResult = new JsonResult(responseResult.Failed("未授权的访问"));

                //如果不包含授权信息
                if (!result)
                {
                    context.Result = jsonResult;
                    return;
                }

                string info = authorization.ToString().Trim();

                //空字符  不包含Bearer  
                if (string.IsNullOrWhiteSpace(info) || !info.Contains(prefix))
                {
                    context.Result = jsonResult;
                    return;
                }

                //token错误
                if (!_authService.ValidateToken(info.Substring(prefix.Length)))
                {
                    context.Result = jsonResult;
                    return;
                }
            }


        }

    }
}
