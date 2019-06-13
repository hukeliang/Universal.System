
using Universal.System.Common;
using Universal.System.Entity.Model;
using Universal.System.Entity.Other;
using Universal.System.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace Universal.System.WebApi.Controllers
{

    [Produces("application/json")]
    //[ApiController]//在Controller上添加了ApiController特性就会启用默认的模型验证返回结果。
    public class UserController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public UserController(IConfiguration configuration, IUserService userService, Service.HomeService homeService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        [Route("api/v1/user/login")]
        [HttpPost]
        [AllowAnonymous]
        //[ServiceFilter(typeof(ValidateTokenAttribute))]
        public IActionResult Login([FromBody, Bind(nameof(UserModel.UserName), nameof(UserModel.Password))] UserModel requestModel)
        {
            _userService.QueryUser("","");
            //BindAttribute特性对JSON数据无效
            IList<ParameterDescriptor> parameterDescriptors = ControllerContext.ActionDescriptor.Parameters;
            //获取BindAttribute
            ParameterDescriptor parameterDescriptor = parameterDescriptors.Where(item => item.BindingInfo.PropertyFilterProvider is BindAttribute).SingleOrDefault();

            BindAttribute bindAttribute = parameterDescriptor.BindingInfo.PropertyFilterProvider as BindAttribute;
            
            ResponseResult responseResult = CommonFactory.CreateResponseResult;
            //Model验证
            foreach (string key in bindAttribute.Include)
            {
                ModelStateEntry modelstate = ModelState[key];
                
                if (modelstate != null && modelstate.ValidationState == ModelValidationState.Invalid)
                {
                    return Ok(responseResult.Failed(modelstate.Errors.FirstOrDefault().ErrorMessage));
                }
            }

            string userName = requestModel.UserName.Trim();
            string password = MD5Encrypting.MD5Encrypt64(MD5Encrypting.MD5Encrypt16(requestModel.Password.Trim()));//两次加密安全性更高

            UserModel user = _userService.QueryUser(userName, password);

            if (user == null || user.IsDelete == DataStatusEnum.Delete)
            {
                return Ok(responseResult.Failed("用户不存在"));
            }
            if (user.Password != password)
            {
                return Ok(responseResult.Failed("用户名或者密码错误"));
            }
            if (user.AccountStatus == DataStatusEnum.PendingReview)
            {
                return Ok(responseResult.Failed("账号待审核通过中"));
            }
            if (user.AccountStatus == DataStatusEnum.Locked)
            {
                return Ok(responseResult.Failed("账号已被锁定"));
            }
            if (user.AccountStatus == DataStatusEnum.Forbidden)
            {
                return Ok(responseResult.Failed("账号已被禁用"));
            }

            return Ok(responseResult.ResponseData(_userService.CreateToken(userName)));

        }


    }
}
