﻿using System;
using System.Collections.Generic;
using System.Text;
using Universal.System.Entity.Model;

namespace Universal.System.DataAccess.Interface
{
    public interface IMenuDataAccess
    {
        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="menuModel"></param>
        /// <returns></returns>
        bool AddMenu(MenuModel menuModel);

        /// <summary>
        /// 根据id删除菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool DeleteMenu(int? id);

        /// <summary>
        /// 修改菜单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool UpdateMenu(MenuModel menuModel);
    }
}
