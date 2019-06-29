using System;
using System.ComponentModel.DataAnnotations;

namespace Universal.System.Entity.ContextModel
{
    /// <summary>
    /// 用户登陆数据载体
    /// </summary>
    [Serializable]
    public class LoginRequestModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required(ErrorMessage = "用户名不能为空")]
        [StringLength(12, MinimumLength = 5, ErrorMessage = "用户名长度为{2}至{1}个字符")]
        public string Username { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        [Required(ErrorMessage = "密码不能为空")]
        [StringLength(16, MinimumLength = 8, ErrorMessage = "密码长度为{2}至{1}个字符")]
        public string Password { get; set; }

    }
}
