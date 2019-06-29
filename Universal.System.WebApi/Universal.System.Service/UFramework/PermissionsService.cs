using System.Collections.Generic;
using System.Linq;
using Universal.System.Common.CustomAttribute;
using Universal.System.DataAccess.Interface;
using Universal.System.Entity.ContextModel;
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
        List<PermissionsModel> IPermissionsService.GetPermissions(int id)
        {
            IQueryable<PermissionsModel> permissions = _permissionsDataAccess.GetPermissions(id);

            return permissions.ToList();
        }
        /// <summary>
        /// 判读权限值是否存在
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        public bool GetPermissionsIsExist(string value)
        {
            PermissionsModel permissions = _permissionsDataAccess.GetPermissions(value);

            return permissions == null;
        }

        /// <summary>
        /// 添加权限值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool AddPermissions(PermissionsRequestModel permissionsRequest)
        {
            PermissionsModel permissions = new PermissionsModel()
            {
                PermissionsName = permissionsRequest.PermissionsName,
                PermissionsValue = permissionsRequest.PermissionsValue,
                Remark = permissionsRequest.Remark,
            };
            return _permissionsDataAccess.AddPermissions(permissions);
        }
    }
}
