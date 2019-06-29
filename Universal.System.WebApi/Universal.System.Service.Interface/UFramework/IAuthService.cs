using System.Collections.Generic;
using Universal.System.Entity.ContextModel;

namespace Universal.System.Service.Interface
{
    /// <summary>
    /// 提供用户身份授权服务
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// 创建用户Token
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        string CreateToken(AuthContext authContext);

        /// <summary>
        /// 验证token有效性
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        bool ValidateToken(string token);

        /// <summary>
        /// 获取当前请求用户上下文信息
        /// </summary>
        /// <returns></returns>
        AuthContext GetUserAuthContext();
    }
}
