using System;
using System.Collections.Generic;
using System.Text;

namespace Universal.System.Entity.Enum
{
    /// <summary>
    /// 账户状态
    /// </summary>
    public enum AccountStatusEnum
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
    }
}
