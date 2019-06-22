using Universal.System.DataAccess.Interface;
using Universal.System.Entity.Model;
using Universal.System.Service.Interface;

namespace Universal.System.Service
{
    /// <summary>
    /// 菜单
    /// </summary>
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
    }
}
