using System.Collections.Generic;
using Universal.System.Common.CustomAttribute;
using Universal.System.DataAccess.Interface;
using Universal.System.Entity.Model;
using Dapper;
using System.Linq;
using System.Threading.Tasks;

namespace Universal.System.DataAccess
{
    /// <summary>
    /// 权限
    /// </summary>
    [DependencyRegister(Type = RegisterType.InstancePerLifetimeScope)]
    public class PermissionsDataAccess : IPermissionsDataAccess
    {
        private readonly IDataAccessBase<UserModel> _dataAccessBase;

        public PermissionsDataAccess(IDataAccessBase<UserModel> dataAccessBase)
        {
            _dataAccessBase = dataAccessBase;
        }

        /// <summary>
        /// 根据用户id获取权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        async Task<List<PermissionsModel>> IPermissionsDataAccess.GetPermissionsByUserIdAsync(int id)
        {
            string sql = @"select PTB.* from Sys.RoleTB RTB 
                           left join Sys.RolePermissions RPTB on RTB.id = RPTB.RoleID 
                           left join Sys.PermissionsTB    PTB on PTB.id = RPTB.PermissionsID
                           where RTB.id in(select RTB.id from Sys.UserTB UTB 
                                           left join Sys.UserRoleTB URTB on UTB.id = URTB.UserID 
                                           left join Sys.RoleTB      RTB on RTB.id = URTB.RoleID
                                           where UTB.id=@UID)
                           union select PTB.* from Sys.UserTB UTB 
                           left join Sys.UserPermissions UPTB on UTB.id = UPTB.UserID 
                           left join Sys.PermissionsTB    PTB on PTB.id = UPTB.PermissionsID
                           where UTB.id=@UID)";

            IEnumerable<PermissionsModel> result = await _dataAccessBase.DBConnection.QueryAsync<PermissionsModel>(sql, new { UID = id });

            return result.ToList();
        }
    }
}
