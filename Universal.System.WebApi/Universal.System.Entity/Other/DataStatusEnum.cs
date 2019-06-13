using System;
using System.Collections.Generic;
using System.Text;

namespace Universal.System.Entity.Other
{
    /// <summary>
    /// 数据状态
    /// </summary>
    public enum DataStatusEnum
    {
        /// <summary>
        /// 正常
        /// </summary>
        Normal,
        
        /// <summary>
        /// 待审核
        /// </summary>
        PendingReview,

        /// <summary>
        /// 锁定
        /// </summary>
        Locked,
        
        /// <summary>
        /// 禁用
        /// </summary>
        Forbidden,

        /// <summary>
        /// 已删除
        /// </summary>
        Delete,
    }
}
