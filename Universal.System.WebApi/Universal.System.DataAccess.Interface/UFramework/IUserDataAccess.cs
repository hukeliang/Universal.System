using Universal.System.Entity.ContextModel;
using Universal.System.Entity.Model;

namespace Universal.System.DataAccess.Interface
{
    /// <summary>
    /// 用户接口
    /// </summary>
    public interface IUserDataAccess
    {
        /// <summary>
        /// 根据用户名密码查询用户
        /// </summary>
        /// <param name="userLoginDto"></param>
        /// <returns></returns>
        UserModel GetUser(LoginRequestModel loginRequest);
    }
}
