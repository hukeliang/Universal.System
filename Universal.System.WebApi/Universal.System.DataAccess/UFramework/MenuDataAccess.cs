using System;
using System.Collections.Generic;
using Universal.System.Common.CustomAttribute;
using Universal.System.DataAccess.Interface;
using Universal.System.Entity.Model;

namespace Universal.System.DataAccess
{
    /// <summary>
    /// 菜单
    /// </summary>
    [DependencyRegister(Type = RegisterType.InstancePerDependency)]
    public class MenuDataAccess : IMenuDataAccess
    {
        private readonly IDataAccessBase<MenuModel> _dbContext;

        public MenuDataAccess(IDataAccessBase<MenuModel> dataAccessBase)
        {
            _dbContext = dataAccessBase;
            Console.Out.WriteLine($"{nameof(MenuDataAccess)}对象被构建");
        }
        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="menuModel"></param>
        /// <returns></returns>
        public bool AddMenu(MenuModel menuModel)
        {
            return _dbContext.Insert(menuModel);
        }
        /// <summary>
        /// 根据id删除菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteMenu(int id)
        {
            return _dbContext.Delete(id);
        }

        public List<MenuModel> GetMenu(int id)
        {
            
            return null;
        }
        /// <summary>
        /// 修改菜单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateMenu(MenuModel menuModel, int id)
        {
            return _dbContext.Update(menuModel, id);
        }
    }
}
