using System.Linq;
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
        /// <returns>IQueryable</returns>
        IQueryable<PermissionsModel> GetPermissions(int id);

        /// <summary>
        /// 判读权限值是否存在
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        PermissionsModel GetPermissions(string value);

        /// <summary>
        /// 添加权限值
        /// </summary>
        /// <param name="userPermissionsDto"></param>
        /// <returns></returns>
        bool AddPermissions(PermissionsModel permissions);
    }
}
