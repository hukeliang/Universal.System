using System;
using System.ComponentModel.DataAnnotations;

namespace Universal.System.Entity.ContextModel
{
    /// <summary>
    /// 用户权限数据载体
    /// </summary>
    [Serializable]
    public class PermissionsRequestModel
    {
        /// <summary>
        /// 权限名字
        /// </summary>
        [Required(ErrorMessage = "权限名不能为空")]
        [MaxLength(20, ErrorMessage = "权限名最多接受{0}个字符")]
        public string PermissionsName { get; set; }
        /// <summary>
        /// 权限值
        /// </summary>
        [Required(ErrorMessage = "权限值不能为空")]
        [MaxLength(100, ErrorMessage = "权限值最多接受{0}个字符")]
        [RegularExpression(@"/^[\u4e00-\u9fa5]{0,}$/", ErrorMessage = "权限值不能包含中文字符")]
        public string PermissionsValue { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Required(ErrorMessage = "备注不能为空")]
        [MaxLength(200, ErrorMessage = "备注最多接受{0}个字符")]
        public string Remark { get; set; }
    }
}
