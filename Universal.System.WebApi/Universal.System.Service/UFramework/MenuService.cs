using Universal.System.Common.CustomAttribute;
using Universal.System.DataAccess.Interface;
using Universal.System.Entity.Model;
using Universal.System.Service.Interface;

namespace Universal.System.Service
{
    /// <summary>
    /// 菜单
    /// </summary>
    [DependencyRegister(Type = RegisterType.InstancePerDependency)]
    public class MenuService : IMenuService
    {
        private readonly IMenuDataAccess _menuDataAccess;

        public MenuService(IMenuDataAccess menuDataAccess)
        {
            _menuDataAccess = menuDataAccess;
        }

        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="menuModel"></param>
        /// <returns></returns>
        bool IMenuService.AddMenu(MenuModel menuModel)
        {
            return _menuDataAccess.AddMenu(menuModel);
        }

        /// <summary>
        /// 根据id删除菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool IMenuService.DeleteMenu(int? id)
        {
            return _menuDataAccess.DeleteMenu(id);
        }

        /// <summary>
        /// 修改菜单信息
        /// </summary>
        /// <param name="menuModel"></param>
        /// <returns></returns>
        bool IMenuService.UpdateMenu(MenuModel menuModel)
        {
            return _menuDataAccess.UpdateMenu(menuModel);
        }
    }
}
