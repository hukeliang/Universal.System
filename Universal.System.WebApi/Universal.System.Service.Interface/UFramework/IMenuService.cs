using System;
using System.Collections.Generic;
using System.Text;
using Universal.System.Entity.Model;

namespace Universal.System.Service.Interface
{
    /// <summary>
    /// 菜单
    /// </summary>
    public interface IMenuService
    {
        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="menuModel"></param>
        /// <returns></returns>
        bool AddMenu(MenuModel menuModel);
    }
}
