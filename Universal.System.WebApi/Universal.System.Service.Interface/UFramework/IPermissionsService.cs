using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Universal.System.Entity.Model;

namespace Universal.System.Service.Interface
{
    /// <summary>
    /// 权限服务
    /// </summary>
    public interface IPermissionsService
    {
        /// <summary>
        /// 根据用户Id查询权限值
        /// </summary>
        /// <returns></returns>
       Task<List<PermissionsModel>> GetPermissionsByUserIdAsync(int id);
    }
}
