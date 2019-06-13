using Universal.System.Entity.Other;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Universal.System.Entity.Model
{
    [Serializable]
    [Table("Sys.UserTB")]
    public  class UserModel : ModelBase
    {
        /// <summary>
        /// 登陆用户名
        /// </summary>
        [Required(ErrorMessage = "用户名不能为空")]
        [StringLength(15, MinimumLength = 5, ErrorMessage = "用户名长度为{0}至{1}个字符")]
        [MaxLength(50)]
        public string UserName { get; set; }

        /// <summary>
        /// 登陆密码
        /// </summary>
        [Required(ErrorMessage = "密码不能为空")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "密码长度为{0}至{1}个字符")]
        [MaxLength(50)]
        public string Password { get; set; }

        /// <summary>
        /// 确认登陆密码
        /// </summary>
        [Compare(nameof(Password), ErrorMessage = "两次密码输入不一致")]
        [NotMapped]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// 登陆邮箱
        /// </summary>
        [Required(ErrorMessage = "邮箱不能为空！")]
        [RegularExpression(@"/^[\w!#$%&'*+/=?^_`{|}~-]+(?:\.[\w!#$%&'*+/=?^_`{|}~-]+)*@(?:[\w](?:[\w-]*[\w])?\.)+[\w](?:[\w-]*[\w])?$/", ErrorMessage = "邮箱格式错误")]
        [MaxLength(100, ErrorMessage = "邮箱最多接受{0}个字符")]
        public string Email { get; set; }

        /// <summary>
        /// 账户状态
        /// </summary>
        [EnumDataType(typeof(DataStatusEnum))]
        public DataStatusEnum AccountStatus { get; set; } = DataStatusEnum.Normal;

        /// <summary>
        /// 一个用户对应多个角色
        /// 一个角色分给多个用户
        /// </summary>
        public virtual ICollection<UserRoleModel> UserRoleCollection { get; set; }

        /// <summary>
        /// 一个用户对应多个权限
        /// 一个权限分给多个用户
        /// </summary>
        public virtual ICollection<UserPermissionsModel> UserPermissionsCollection { get; set; }

    }
}
