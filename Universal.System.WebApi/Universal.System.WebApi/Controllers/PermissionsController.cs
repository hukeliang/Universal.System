using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Universal.System.WebApi.Controllers
{
    [Route("api/permissions")]
    public class PermissionsController : ControllerBase
    {
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetPermissions(int? id)
        {
            //获取用户权限值
            return Ok();
        }

        [HttpPost]
        public IActionResult AddPermissions()
        {
            //新增用户权限值
            return Ok();
        }


    }
}