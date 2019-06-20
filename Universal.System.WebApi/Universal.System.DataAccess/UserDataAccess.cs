using Dapper;
using Universal.System.Common.CustomAttribute;
using Universal.System.DataAccess.Interface;
using Universal.System.Entity.Model;
using System.Data;
using System;

namespace Universal.System.DataAccess
{
    [DependencyRegister(Type = RegisterType.InstancePerLifetimeScope)]
    public class UserDataAccess : IUserDataAccess
    {
        private readonly IDataAccessBase<UserModel> _dataAccessBase;

        private readonly IDbConnection _dbConnection;

        public UserDataAccess(IDataAccessBase<UserModel> dataAccessBase)
        {
            _dataAccessBase = dataAccessBase;
            _dbConnection = dataAccessBase.DBConnection;
            Console.Out.WriteLine($"{nameof(UserDataAccess)}对象被构建");

        }

        /// <summary>
        /// 根据用户名 密码 查询用户信息
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        UserModel IUserDataAccess.QueryUser(string userName, string password) =>
            _dbConnection.QuerySingle<UserModel>("SELECT * FROM Sys.UserTB WHERE UserName=@UserName AND Password=@Password", new UserModel()
            {
                UserName = userName,
                Password = password
            });

        UserModel IUserDataAccess.QueryUser() => new UserModel();

        public dynamic QueryUser(string f)
        {
            dynamic fasdf= _dbConnection.Query<dynamic>("SELECT * FROM Usertb_US");

            return fasdf;
        }

    }
}
