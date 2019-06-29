using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Universal.System.Entity.Model
{
    [Serializable]
    [Table("Sys.MenuTb")]
    public class MenuModel : BaseModel
    {
        /// <summary>
        /// 菜单名字
        /// </summary>
        [Required(ErrorMessage = "菜单名字不能为空")]
        [MaxLength(20, ErrorMessage = "菜单名字最多接受{0}个字符")]
        public string MenuName { get; set; }

        /// <summary>
        /// 菜单Url地址
        /// </summary>
        [Required(ErrorMessage = "菜单地址不能为空")]
        [MaxLength(100, ErrorMessage = "菜单地址最多接受{0}个字符")]
        public string MenuUrl { get; set; }

        /// <summary>
        /// 菜单图标
        /// </summary>
        [Required(ErrorMessage = "菜单图标地址不能为空")]
        [MaxLength(100, ErrorMessage = "菜单图标地址最多接受{0}个字符")]
        public string MenuIcon{ get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Remark { get; set; }
    
        /// <summary>
        /// 父类菜单ID
        /// </summary>
        [Required]
        public int ParentID { get; set; }

        /// <summary>
        /// 菜单对应权限ID
        /// </summary>
        [Required]
        [ForeignKey(nameof(PermissionsModel))]
        public int PermissionsID { get; set; }

        public virtual PermissionsModel Permissions { get; set; }
    }
}
