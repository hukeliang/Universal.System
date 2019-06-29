using System;

namespace Universal.System.Entity.ContextModel
{
    /// <summary>
    /// 授权用户上下文
    /// </summary>
    [Serializable]
    public partial class AuthContext
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int ID { get; set; }
        ///<summary>
        /// 登录名
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public string Role { get; set; }
    }
}
