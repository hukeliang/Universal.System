using Universal.System.Entity.Model;
using System;

namespace Universal.System.Service.Interface
{
    public interface IUserService
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
        /// <summary>
        /// 根据用户名密码查询用户
        /// </summary>
        /// <param name="account">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        UserModel QueryUser(string account, string password);


    }
}
