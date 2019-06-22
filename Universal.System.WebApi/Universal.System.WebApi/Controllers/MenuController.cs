using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Universal.System.Common;
using Universal.System.Entity.Model;
using Universal.System.Service.Interface;
using Universal.System.WebApi.Filter;

namespace Universal.System.WebApi.Controllers
{
    /// <summary>
    /// 提供对菜单的 CRUD
    /// </summary>
    [Route("api/menu")]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }
        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetEenu()
        {
            return Ok();
        }
        /// <summary>
        /// 根据id获取菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetEenu(int? id)
        {
            //获取菜单
            return Ok();
        }

        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="menuModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        public IActionResult AddEenu(MenuModel menuModel)
        {
            ResponseResult responseResult = CommonFactory.CreateResponseResult;

            if (_menuService.AddMenu(menuModel))
            {
                return Ok(responseResult.Success("菜单添加成功"));
            }

            return Ok(responseResult.Failed("菜单添加失败"));
        }

        /// <summary>
        /// 更新菜单
        /// </summary>
        /// <param name="menuModel"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult UpdateMenu(MenuModel menuModel)
        {
            return Ok();
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult DeleteMenu(int? id)
        {
            return Ok();
        }
    }
}