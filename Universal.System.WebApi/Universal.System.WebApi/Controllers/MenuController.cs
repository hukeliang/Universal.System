using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Universal.System.Entity.Model;

namespace Universal.System.WebApi.Controllers
{
    /// <summary>
    /// 提供对菜单的 CRUD
    /// </summary>
    [Route("api/menu")]
    public class MenuController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetEenu()
        {
            //获取菜单
            return Ok();
        }

        [HttpPost]
        public IActionResult AddEenu(MenuModel menuModel)
        {
            //新增菜单
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateMenu(MenuModel menuModel)
        {
            //更新菜单
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteMenu(int? id)
        {
            //删除菜单
            return Ok();
        }
    }
}