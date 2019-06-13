using Universal.System.Entity.Other;
using System;
using System.ComponentModel.DataAnnotations;

namespace Universal.System.Entity
{
    public class ModelBase
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        public long ID { get; set; }

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
