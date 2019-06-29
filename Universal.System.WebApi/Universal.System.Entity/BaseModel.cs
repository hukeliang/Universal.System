using Universal.System.Entity.Enum;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Universal.System.Entity
{
    /// <summary>
    /// Model 基类
    /// </summary>
    public class BaseModel
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        [Column(Order = 0)]
        public int ID { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateDate { get; private set; } = DateTime.Now;

        /// <summary>
        /// 软删除 是否删除（0正常，1删除）
        /// </summary>
        [EnumDataType(typeof(DataStatusEnum))]
        public DataStatusEnum IsDelete { get; set; } = DataStatusEnum.Normal;

    }
}
