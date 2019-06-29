using System.Collections.Generic;
using System.Linq;
using Universal.System.Common.CustomAttribute;
using Universal.System.DataAccess.Interface;
using Universal.System.Entity.Model;

namespace Universal.System.DataAccess
{
    /// <summary>
    /// 权限
    /// </summary>
    [DependencyRegister(Type = RegisterType.InstancePerDependency)]
    public class PermissionsDataAccess : IPermissionsDataAccess
    {
        private readonly IDataAccessBase<PermissionsModel> _dbContext;

        public PermissionsDataAccess(IDataAccessBase<PermissionsModel> dataAccessBase)
        {
            _dbContext = dataAccessBase;
        }

        /// <summary>
        /// 添加权限值
        /// </summary>
        /// <param name="permissions"></param>
        /// <returns></returns>
        public bool AddPermissions(PermissionsModel permissions)
        {
            return _dbContext.Insert(permissions);
        }

        /// <summary>
        /// 根据用户id获取权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IQueryable<PermissionsModel> GetPermissions(int id)
        {
            //用户角色的权限
            var userRolePermissions = from ptb in _dbContext.Db.Permissions
                                      join rptb in _dbContext.Db.RolePermissions on ptb.ID equals rptb.RoleID
                                      join rtb in _dbContext.Db.Role on rptb.RoleID equals rtb.ID
                                      where (from rtb_son in _dbContext.Db.Role
                                             join urtb in _dbContext.Db.UserRole on rtb_son.ID equals urtb.RoleID
                                             join utb in _dbContext.Db.User on urtb.UserID equals utb.ID
                                             where utb.ID == id
                                             select rtb_son.ID).Contains(rtb.ID)
                                      select ptb;
            //用户权限
            var userPermissions = from ptb in _dbContext.Db.Permissions
                                  join uptb in _dbContext.Db.UserPermissions on ptb.ID equals uptb.PermissionsID
                                  join utb in _dbContext.Db.User on uptb.UserID equals utb.ID
                                  where utb.ID == id
                                  select ptb;
            //合并
            var result = userRolePermissions.Union(userPermissions);
            return result;
        }

        /// <summary>
        /// 根据权限值获取权限信息
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public PermissionsModel GetPermissions(string value)
        {
            return _dbContext.FindSingle(item => item.PermissionsValue == value);
        }
    }
}
