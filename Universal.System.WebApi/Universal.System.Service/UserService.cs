using Universal.System.Common.CustomAttribute;
using Universal.System.DataAccess.Interface;
using Universal.System.Entity.Model;
using Universal.System.Service.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

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
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        UserModel IUserService.QueryUser(string account, string password)
        {
            return _userDataAccess.QueryUser();
        }
    }
}
