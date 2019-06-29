using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Universal.System.Entity.Model
{
    [Serializable]
    [Table("Sys.PermissionsTb")]
    public class PermissionsModel : BaseModel
    {
        /// <summary>
        /// 权限名字
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string PermissionsName { get; set; }

        /// <summary>
        /// 权限值
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string PermissionsValue { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Remark { get; set; }

        /// <summary>
        /// 一个用户对应多个权限
        /// 一个权限分给多个用户
        /// </summary>
        public virtual ICollection<UserPermissionsModel> UserPermissionsCollection { get; set; }


        public virtual MenuModel Menu { get; set; }
    }
}
