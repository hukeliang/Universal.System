using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Universal.System.Common;
using Universal.System.Entity.Enum;
using Universal.System.Entity.Model;
using Universal.System.Entity.ContextModel;
using Universal.System.Service.Interface;
using Universal.System.WebApi.Filter;

namespace Universal.System.WebApi.Controllers
{
    /// <summary>
    /// 提供用户的身份授权
    /// </summary>
    [Produces("application/json")]
    [Route("api/auth")]
    //[ApiController]//在Controller上添加了ApiController特性就会启用默认的模型验证返回结果。
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public AuthController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        /// <summary>
        /// 验证账号密码 签发token
        /// </summary>
        /// <remarks>
        /// 例子:Post api/auth
        /// </remarks>
        /// <param name="requestModel"></param>
        /// <returns>返回token</returns>
        /// <response code="200">授权成功</response>
        [HttpPost]
        [AllowAnonymous]
        [ValidateRequestModel]
        [ProducesResponseType(200)]
        //[ServiceFilter(typeof(ValidateTokenAttribute))]
        public IActionResult GetAuth([FromBody] LoginRequestModel loginRequest)
        {
            loginRequest.Username = loginRequest.Username.Trim();
            loginRequest.Password = MD5Encrypting.MD5Encrypt64(MD5Encrypting.MD5Encrypt16(loginRequest.Password.Trim()));//两次加密安全性更高

            ResponseResult responseResult = CommonFactory.CreateResponseResult;

            UserModel user = _userService.GetUser(loginRequest);

            if (user == null || user.IsDelete == DataStatusEnum.Delete)
            {
                return Ok(responseResult.Failed("用户名或者密码错误"));
            }
            if (user.AccountStatus == AccountStatusEnum.PendingReview)
            {
                return Ok(responseResult.Failed("账号待审核通过中"));
            }
            if (user.AccountStatus == AccountStatusEnum.Locked)
            {
                return Ok(responseResult.Failed("账号已被锁定"));
            }
            if (user.AccountStatus == AccountStatusEnum.Forbidden)
            {
                return Ok(responseResult.Failed("账号已被禁用"));
            }

            string token = _authService.CreateToken(new AuthContext()
            {
                ID = user.ID,
                Username = user.Username,
                Email = user.Email,
                Role = "testRole",
            });

            return Ok(responseResult.ResponseData(token));
        }
    }
}
