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
