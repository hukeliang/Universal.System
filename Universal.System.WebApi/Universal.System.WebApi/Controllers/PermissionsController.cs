using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Universal.System.Common;
using Universal.System.Entity.Model;
using Universal.System.Entity.ContextModel;
using Universal.System.Service.Interface;
using Universal.System.WebApi.Filter;

namespace Universal.System.WebApi.Controllers
{
    /// <summary>
    /// 权限接口
    /// </summary>
    [Produces("application/json")]
    [Route("api/permissions")]
    public class PermissionsController : ControllerBase
    {
        /// <summary>
        /// 日志记录
        /// </summary>
        private readonly ILogger<PermissionsController> _logger;
        private readonly IPermissionsService _permissionsService;
        private readonly IAuthService _authService;

        public PermissionsController(ILogger<PermissionsController> logger, IPermissionsService permissionsService, IAuthService authService)
        {
            _logger = logger;
            _permissionsService = permissionsService;
            _authService = authService;
        }

        /// <summary>
        /// 获取用户权限值
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetPermissions()
        {
            ResponseResult responseResult = CommonFactory.CreateResponseResult;
            //获取用户上下文信息
            AuthContext authContext = _authService.GetUserAuthContext();
            
            int userId = authContext.ID;
            //获取用户权限值
            List<PermissionsModel> permissions = _permissionsService.GetPermissions(userId);

            return Ok(responseResult.ResponseData(permissions));
        }

        /// <summary>
        /// 添加权限
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateRequestModel]
        public IActionResult AddPermissions([FromBody] PermissionsRequestModel permissionsRequest)
        {
            ResponseResult responseResult = CommonFactory.CreateResponseResult;
            //判读权限值是否存在
            if (_permissionsService.GetPermissionsIsExist(permissionsRequest.PermissionsValue))
            {
                return Ok(responseResult.Failed("当前权限值已经存在"));
            }
            //新增用户权限值
            if (_permissionsService.AddPermissions(permissionsRequest))
            {
                return Ok(responseResult.Success("权限添加成功"));
            }

            return Ok(responseResult.Error("错误：权限添加失败"));

        }


    }
}