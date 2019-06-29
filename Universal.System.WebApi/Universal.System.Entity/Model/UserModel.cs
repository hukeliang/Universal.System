using Universal.System.Entity.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Universal.System.Entity.Model
{
    [Serializable]
    [Table("Sys.UserTb")]
    public  class UserModel : BaseModel
    {
        /// <summary>
        /// 登陆用户名
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        /// <summary>
        /// 登陆密码
        /// </summary>
        [Required]
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
        [EnumDataType(typeof(AccountStatusEnum))]
        public AccountStatusEnum AccountStatus { get; set; } = AccountStatusEnum.PendingReview;

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
