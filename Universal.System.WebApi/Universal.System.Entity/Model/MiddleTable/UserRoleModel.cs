using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Universal.System.Entity.Model
{
    [Serializable]
    [Table("Sys.UserRoleTB")]
    public class UserRoleModel : ModelBase
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [Required]
        public long UserID { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        [Required]
        public long RoleID { get; set; }


        public virtual UserModel UserModel { set; get; }

        public virtual RoleModel RoleModel { set; get; }


    }
}
