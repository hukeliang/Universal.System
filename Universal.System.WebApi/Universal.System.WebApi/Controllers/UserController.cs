using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Universal.System.Service.Interface;

namespace Universal.System.WebApi.Controllers
{

    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// 日志记录
        /// </summary>
        private readonly ILogger<UserController> _logger;
        
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }



    }
}
