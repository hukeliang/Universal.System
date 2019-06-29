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
    [Produces("application/json")]
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
        [ValidateRequestModel]
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
        [ValidateRequestModel]
        public IActionResult UpdateMenu(MenuModel menuModel)
        {
            ResponseResult responseResult = CommonFactory.CreateResponseResult;

            //if (_menuService.UpdateMenu(menuModel))
            //{
            //    return Ok(responseResult.Success("菜单修改成功"));
            //}

            return Ok(responseResult.Failed("菜单修改失败"));
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult DeleteMenu(int? id)
        {
            ResponseResult responseResult = CommonFactory.CreateResponseResult;
            
            //if (_menuService.DeleteMenu(id))
            //{
            //    return Ok(responseResult.Success("菜单删除成功"));
            //}

            return Ok(responseResult.Failed("菜单删除失败"));
        }
    }
}