using System;
using Universal.System.DataAccess.Interface;
using Universal.System.Entity.Model;

namespace Universal.System.DataAccess
{

    public class MenuDataAccess : IMenuDataAccess
    {
        private readonly IDataAccessBase<MenuModel> _dataAccessBase;

        public MenuDataAccess(IDataAccessBase<MenuModel> dataAccessBase)
        {
            _dataAccessBase = dataAccessBase;
            Console.Out.WriteLine($"{nameof(MenuDataAccess)}对象被构建");
        }

        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="menuModel"></param>
        /// <returns></returns>
        bool IMenuDataAccess.AddMenu(MenuModel menuModel) => _dataAccessBase.Insert(menuModel) > 0;
    }
}
