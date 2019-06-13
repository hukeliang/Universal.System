using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Universal.System.Entity.Model
{
    [Serializable]
    [Table("Sys.UserPermissionsTB")]
    public class UserPermissionsModel : ModelBase
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [Required]
        public long UserID { get; set; }

        /// <summary>
        /// 权限ID
        /// </summary>
        [Required]
        public long PermissionsID { get; set; }

        public virtual UserModel Users { set; get; }

        public virtual PermissionsModel Permissions { set; get; }

    }
}
