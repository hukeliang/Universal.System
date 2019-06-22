using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Universal.System.Common.CustomAttribute;
using Universal.System.DataAccess.Interface;
using Universal.System.Service.Interface;

namespace Universal.System.Service
{
    [DependencyRegister(Type = RegisterType.InstancePerDependency)]
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserDataAccess _userDataAccess;

        public AuthService(IConfiguration configuration, IUserDataAccess userDataAccess)
        {
            _configuration = configuration;
            _userDataAccess = userDataAccess;
        }

        /// <summary>
        /// 创建Token
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        string IAuthService.CreateToken(string userName, string role)
        {
            /**
             * exp: Expiration Time。 token 过期时间，Unix 时间戳格式
             * .NET Core的JwtSecurityToken类承担了繁重的工作，并实际创建了令牌.
             * Claims (Payload)
             * Claims 部分包含了一些跟这个 token 有关的重要信息。 JWT
             * iss: The issuer of the token，token 是给谁的
             * sub: The subject of the token，token 主题
             * exp: Expiration Time。 token 过期时间，Unix 时间戳格
             * iat: Issued At。 token 创建时间， Unix 时间戳格式
             * jti: JWT ID。针对当前 token 的唯一标识
             * 除了规定的字段外，可以包含其他任何 JSON 兼容的字段。
             */

            //密钥
            string tokenSecret = _configuration["Token:Secret"];

            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSecret));

            JwtSecurityToken token = new JwtSecurityToken(claims: new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Iss, _configuration["Token:Issuer"]),
                new Claim(JwtRegisteredClaimNames.Aud, _configuration["Token:Audience"]),
                new Claim(JwtRegisteredClaimNames.Sub, userName),
                new Claim(JwtRegisteredClaimNames.Jti, $"{ Guid.NewGuid().ToString("N") }"),
                new Claim(JwtRegisteredClaimNames.Iat, $"{ DateTime.UtcNow.ToUniversalTime() }", ClaimValueTypes.Integer64),
                new Claim(JwtRegisteredClaimNames.Nbf, $"{ new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds() }") ,//可用时间起始
                new Claim(JwtRegisteredClaimNames.Exp, $"{ new DateTimeOffset(DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Token:Exp"]))).ToUnixTimeSeconds() }"),//可用时间结束
                new Claim(ClaimTypes.Name, userName), //用户名
                new Claim(ClaimTypes.Role, role), //角色
            },
            signingCredentials: new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256)); //加密算法

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            return jwtSecurityTokenHandler.WriteToken(token);
        }

        /// <summary>
        /// 验证token是否正确
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        bool IAuthService.ValidateToken(string token)
        {
            string[] tokenArray = token.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

            //截断字符成数组长度不够3位不是jwt token格式 直接返回
            if (tokenArray.Length != 3)
            {
                return false;
            }

            HMACSHA256 hs256 = new HMACSHA256(Encoding.ASCII.GetBytes(_configuration["Token:Secret"]));

            Dictionary<string, object> header = JsonConvert.DeserializeObject<Dictionary<string, object>>(Base64UrlEncoder.Decode(tokenArray[0]));
            Dictionary<string, object> payLoad = JsonConvert.DeserializeObject<Dictionary<string, object>>(Base64UrlEncoder.Decode(tokenArray[1]));

            //首先验证签名是否正确（必须的）
            bool signature = string.Equals(tokenArray[2], Base64UrlEncoder.Encode(hs256.ComputeHash(Encoding.UTF8.GetBytes(string.Concat(tokenArray[0], ".", tokenArray[1])))));
            if (!signature)
            {
                return false;//签名不正确直接返回
            }

            return payLoad["iss"]?.ToString() == _configuration["Token:Issuer"]
                && payLoad["aud"]?.ToString() == _configuration["Token:Audience"];
        }
    }
}
