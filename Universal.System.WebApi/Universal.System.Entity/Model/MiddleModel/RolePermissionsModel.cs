using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Universal.System.Entity.Model
{
    [Serializable]
    [Table("Sys.RolePermissionsTb")]
    public class RolePermissionsModel : BaseModel
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        [Required]
        public int RoleID { get; set; }

        /// <summary>
        /// 权限ID
        /// </summary>
        [Required]
        public int PermissionsID { get; set; }

        public virtual RoleModel Roles { set; get; }

        public virtual PermissionsModel Permissions { set; get; }

    }
}
