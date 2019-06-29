using System;
using Universal.System.Common.CustomAttribute;
using Universal.System.DataAccess.Interface;
using Universal.System.Entity.ContextModel;
using Universal.System.Entity.Model;

namespace Universal.System.DataAccess
{
    [DependencyRegister(Type = RegisterType.InstancePerLifetimeScope)]
    public class UserDataAccess : IUserDataAccess
    {
        private readonly IDataAccessBase<UserModel> _dbContext;
        

        public UserDataAccess(IDataAccessBase<UserModel> dataAccessBase)
        {
            _dbContext = dataAccessBase;

            Console.Out.WriteLine($"{nameof(UserDataAccess)}对象被构建");

        }
        /// <summary>
        ///  根据用户名 密码 查询用户信息
        /// </summary>
        /// <param name="userLoginDto"></param>
        /// <returns></returns>
        public UserModel GetUser(LoginRequestModel loginRequest)
        {
            return _dbContext.FindSingle(item => item.Username == loginRequest.Username && item.Password == loginRequest.Password);
        }
    }
}
