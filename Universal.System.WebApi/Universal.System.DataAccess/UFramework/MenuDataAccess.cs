using System;
using Universal.System.DataAccess.Interface;
using Universal.System.Entity.Model;
using Dapper;
using Universal.System.Common.CustomAttribute;

namespace Universal.System.DataAccess
{
    /// <summary>
    /// 菜单
    /// </summary>
    [DependencyRegister(Type = RegisterType.InstancePerDependency)]
    public class MenuDataAccess : IMenuDataAccess
    {
        private readonly IDataAccessBase<MenuModel> _dataAccessBase;

        public MenuDataAccess(IDataAccessBase<MenuModel> dataAccessBase)
        {
            _dataAccessBase = dataAccessBase;
            Console.Out.WriteLine($"{nameof(MenuDataAccess)}对象被构建");
        }


        MenuModel IMenuDataAccess.GetMenu()
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
            return null;

            //MenuModel menu = _dataAccessBase.DBConnection.QueryAsync<UserModel, PermissionsModel, UserModel>(sql, (menuItem, permissionsItem) =>
            //{
            //    menuItem.UserPermissionsCollection.Add(permissionsItem)
            //}, new
            //{
            //    UID = 1,
            //});

        }

        /// <summary>
        /// 
        /// 
        /// 添加菜单
        /// </summary>
        /// <param name="menuModel"></param>
        /// <returns></returns>
        bool IMenuDataAccess.AddMenu(MenuModel menuModel)
        {
            return _dataAccessBase.Insert(menuModel) > 0;
        }

        /// <summary>
        /// 根据id删除菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool IMenuDataAccess.DeleteMenu(int? id)
        {
            return _dataAccessBase.Delete<int?>(id) > 0;
        }



        /// <summary>
        /// 修改菜单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool IMenuDataAccess.UpdateMenu(MenuModel menuModel)
        {
            return _dataAccessBase.Update(menuModel) > 0;
        }
    }
}
