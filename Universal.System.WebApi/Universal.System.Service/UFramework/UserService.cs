using Universal.System.Common.CustomAttribute;
using Universal.System.DataAccess.Interface;
using Universal.System.Entity.ContextModel;
using Universal.System.Entity.Model;
using Universal.System.Service.Interface;

namespace Universal.System.Service
{
    [DependencyRegister(Type = RegisterType.InstancePerDependency)]
    public class UserService : IUserService
    {
        private readonly IUserDataAccess _userDataAccess;

        public UserService(IUserDataAccess userDataAccess)
        {
            _userDataAccess = userDataAccess;
        }

        /// <summary>
        /// 根据用户名密码查询用户信息
        /// </summary>
        /// <param name="userLoginDto"></param>
        /// <returns></returns>
        public UserModel GetUser(LoginRequestModel loginRequest)
        {
            return _userDataAccess.GetUser(loginRequest);
        }
    }
}
