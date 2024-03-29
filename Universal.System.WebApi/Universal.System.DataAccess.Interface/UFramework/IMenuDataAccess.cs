﻿using System;
using System.Collections.Generic;
using System.Text;
using Universal.System.Entity.Model;

namespace Universal.System.DataAccess.Interface
{
    public interface IMenuDataAccess
    {
        /// <summary>
        /// 根据用户信息获取响应权限值
        /// </summary>
        /// <returns></returns>
        List<MenuModel> GetMenu(int id);
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
        bool DeleteMenu(int id);

        /// <summary>
        /// 修改菜单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool UpdateMenu(MenuModel menuModel,int id);
    }
}
