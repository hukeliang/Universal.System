using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Universal.System.Entity.Model
{
    [Serializable]
    [Table("Sys.UserRoleTb")]
    public class UserRoleModel : BaseModel
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [Required]
        public int UserID { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        [Required]
        public int RoleID { get; set; }


        public virtual UserModel User { set; get; }

        public virtual RoleModel Role{ set; get; }


    }
}
