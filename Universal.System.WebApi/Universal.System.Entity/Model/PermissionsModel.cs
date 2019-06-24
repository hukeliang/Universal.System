using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Universal.System.Entity.Model
{
    [Serializable]
    [Table("Sys.PermissionsTB")]
    public class PermissionsModel : ModelBase
    {
        /// <summary>
        /// 权限名字
        /// </summary>
        [Required(ErrorMessage = "权限名字不能为空")]
        [MaxLength(20, ErrorMessage = "权限名字最多接受{0}个字符")]
        public string PermissionsName { get; set; }

        /// <summary>
        /// 权限值
        /// </summary>
        [Required(ErrorMessage = "权限值不能为空")]
        [MaxLength(100, ErrorMessage = "权限值最多接受{0}个字符")]
        public string PermissionsValue { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Required]
        [MaxLength(300)]
        public string Remark { get; set; } = string.Empty;

        /// <summary>
        /// 隶属哪个菜单
        /// </summary>
        [Required]
        public int MenuID { get; set; }
        /// <summary>
        /// 一个用户对应多个权限
        /// 一个权限分给多个用户
        /// </summary>
        public virtual ICollection<UserPermissionsModel> UserPermissionsCollection { get; set; }

    }
}
