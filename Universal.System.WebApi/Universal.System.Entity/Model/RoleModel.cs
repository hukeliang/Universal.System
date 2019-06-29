using Universal.System.Entity.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Universal.System.Entity.Model
{
    [Serializable]
    [Table("Sys.RoleTb")]
    public class RoleModel : BaseModel
    {
        /// <summary>
        /// 角色名字
        /// </summary>
        [Required(ErrorMessage = "角色名字不能为空")]
        [MaxLength(20, ErrorMessage = "角色名字最多接受{0}个字符")]
        public string RoleName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Required]
        [MaxLength(300)]
        public string Remark { get; set; } = string.Empty;

        /// <summary>
        /// 角色状态
        /// </summary>
        [EnumDataType(typeof(RoleStatusEnum))]
        public RoleStatusEnum RoleStatus { get; set; } = RoleStatusEnum.Forbidden;

        /// <summary>
        /// 一个用户对应多个角色  
        /// 一个角色分给多个用户
        /// </summary>
        public virtual ICollection<UserRoleModel> UserRoleCollection { get; set; }

        /// <summary>
        /// 一个角色对应多个权限
        /// 一个权限分给多个角色
        /// </summary>
        public virtual ICollection<RolePermissionsModel> RolePermissionsCollection { get; set; }


    }
}
