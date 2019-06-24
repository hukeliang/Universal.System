using System.Collections.Generic;
using System.Threading.Tasks;
using Universal.System.Entity.Model;

namespace Universal.System.DataAccess.Interface
{
    /// <summary>
    /// 权限
    /// </summary>
    public interface IPermissionsDataAccess
    {
        /// <summary>
        /// 根据用户Id查询权限值
        /// </summary>
        /// <returns></returns>
        Task<List<PermissionsModel>> GetPermissionsByUserIdAsync(int id);
    }
}
