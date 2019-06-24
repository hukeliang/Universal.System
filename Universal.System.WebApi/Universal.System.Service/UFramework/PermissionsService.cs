using System.Collections.Generic;
using System.Threading.Tasks;
using Universal.System.Common.CustomAttribute;
using Universal.System.DataAccess.Interface;
using Universal.System.Entity.Model;
using Universal.System.Service.Interface;

namespace Universal.System.Service
{
    /// <summary>
    /// 权限服务
    /// </summary>
    [DependencyRegister(Type = RegisterType.InstancePerLifetimeScope)]
    public class PermissionsService : IPermissionsService
    {
        private readonly IPermissionsDataAccess _permissionsDataAccess;

        public PermissionsService(IPermissionsDataAccess permissionsDataAccess)
        {
            _permissionsDataAccess = permissionsDataAccess;
        }

        /// <summary>
        ///  根据用户id获取权限
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        async Task<List<PermissionsModel>> IPermissionsService.GetPermissionsByUserIdAsync(int id)
        {
            return await _permissionsDataAccess.GetPermissionsByUserIdAsync(id);
        }
    }
}
