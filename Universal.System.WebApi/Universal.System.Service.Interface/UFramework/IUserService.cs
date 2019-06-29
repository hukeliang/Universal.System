using Universal.System.Entity.ContextModel;
using Universal.System.Entity.Model;

namespace Universal.System.Service.Interface
{
    public interface IUserService
    {
        /// <summary>
        /// 根据用户名密码查询用户
        /// </summary>
        /// <param name="account">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        UserModel GetUser(LoginRequestModel loginRequest);


    }
}
