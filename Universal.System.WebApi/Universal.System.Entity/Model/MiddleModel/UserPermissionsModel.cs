using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Universal.System.Entity.Model
{
    [Serializable]
    [Table("Sys.UserPermissionsTb")]
    public class UserPermissionsModel: BaseModel
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [Required]
        public int UserID { get; set; }

        /// <summary>
        /// 权限ID
        /// </summary>
        [Required]
        public int PermissionsID { get; set; }

        public virtual UserModel User { set; get; }

        public virtual PermissionsModel Permissions { set; get; }

    }
}
