using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Universal.System.Common;
using Universal.System.Common.CustomAttribute;
using Universal.System.DataAccess.Interface;
using Universal.System.Entity.ContextModel;
using Universal.System.Service.Interface;

namespace Universal.System.Service
{
    [DependencyRegister(Type = RegisterType.InstancePerDependency)]
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserDataAccess _userDataAccess;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IConfiguration configuration, IUserDataAccess userDataAccess, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _userDataAccess = userDataAccess;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 创建Token
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public string CreateToken(AuthContext authContext)
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

            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Secret"])); //密钥

            List<Claim> claimsInfo = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Iss, _configuration["Token:Issuer"]),
                new Claim(JwtRegisteredClaimNames.Aud, _configuration["Token:Audience"]),
                new Claim(JwtRegisteredClaimNames.Jti, $"{ Guid.NewGuid().ToString("N") }"),
                new Claim(JwtRegisteredClaimNames.Iat, $"{ DateTime.UtcNow.ToUniversalTime() }", ClaimValueTypes.Integer64),
                new Claim(JwtRegisteredClaimNames.Nbf, $"{ new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds() }") ,//可用时间起始
                new Claim(JwtRegisteredClaimNames.Exp, $"{ new DateTimeOffset(DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Token:Exp"]))).ToUnixTimeSeconds() }"),//可用时间结束
                new Claim(JwtRegisteredClaimNames.Sub, DESEncrypting.Encrypt(authContext.ID.ToString(),_configuration["DESKey"])),//用户id使用des加密
                new Claim(JwtRegisteredClaimNames.UniqueName, authContext.Username), //用户名
                new Claim(JwtRegisteredClaimNames.Email, authContext.Email), 
                new Claim(ClaimTypes.Role, authContext.Role),//角色
            };

            JwtSecurityToken token = new JwtSecurityToken(claims: claimsInfo, signingCredentials: new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256)); //加密算法

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            return jwtSecurityTokenHandler.WriteToken(token);
        }

        /// <summary>
        /// 验证token是否正确
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool ValidateToken(string token)
        {
            string[] tokenArray = token.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
           
            if (tokenArray.Length != 3) //截断字符成数组长度不够3位不是jwt token格式 直接返回
            {
                return false;
            }
            HMACSHA256 hs256 = new HMACSHA256(Encoding.ASCII.GetBytes(_configuration["Token:Secret"]));

            Dictionary<string, string> payLoad = JsonConvert.DeserializeObject<Dictionary<string, string>>(Base64UrlEncoder.Decode(tokenArray[1]));
           
            //首先验证签名是否正确（必须的）
            bool signature = string.Equals(tokenArray[2], Base64UrlEncoder.Encode(hs256.ComputeHash(Encoding.UTF8.GetBytes(string.Concat(tokenArray[0], ".", tokenArray[1])))));

            if (!signature)
            {
                return false;//签名不正确直接返回
            }

            long nowTime = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();

            long.TryParse(payLoad[JwtRegisteredClaimNames.Exp], out long tokenExp);
            long.TryParse(payLoad[JwtRegisteredClaimNames.Exp], out long tokenNbf);

            bool result = payLoad[JwtRegisteredClaimNames.Iss] == _configuration["Token:Issuer"]
                       && payLoad[JwtRegisteredClaimNames.Aud] == _configuration["Token:Audience"]
                       && tokenExp >= nowTime;

            //每次解析放到HttpContext.User中
            if (result)
            {
                List<Claim> claims = new List<Claim>();
                foreach (var item in payLoad)
                {
                    claims.Add(new Claim(item.Key, item.Value));
                }
                _httpContextAccessor.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(claims)); ;
            }

            return result;
        }

        /// <summary>
        /// 获取当前请求用户上下文信息
        /// </summary>
        /// <returns></returns>
        public AuthContext GetUserAuthContext()
        {
            ClaimsPrincipal claimsPrincipal = _httpContextAccessor.HttpContext.User;
            //des解密
            int.TryParse(DESEncrypting.Decrypt(claimsPrincipal.FindFirstValue(JwtRegisteredClaimNames.Sub), _configuration["DESKey"]), out int userId);

            AuthContext authContext = new AuthContext()
            {
                ID = userId,
                Username = claimsPrincipal.FindFirstValue(JwtRegisteredClaimNames.UniqueName),
                Email = claimsPrincipal.FindFirstValue(JwtRegisteredClaimNames.Email),
                Role = claimsPrincipal.FindFirstValue(ClaimTypes.Role),
            };

            return authContext;
        }

    }
}
