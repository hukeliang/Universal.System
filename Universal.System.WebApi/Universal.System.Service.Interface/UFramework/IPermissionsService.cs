using System.Collections.Generic;
using Universal.System.Entity.Model;
using Universal.System.Entity.ContextModel;

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
        List<PermissionsModel> GetPermissions(int id);

        /// <summary>
        /// 判读权限值是否存在
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        bool GetPermissionsIsExist(string value);

        /// <summary>
        /// 添加权限值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        bool AddPermissions(PermissionsRequestModel permissionsRequest);
    }
}
