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
        string CreateToken(string userName, string role = "");

        /// <summary>
        /// 验证token有效性
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        bool ValidateToken(string token);
    }
}
