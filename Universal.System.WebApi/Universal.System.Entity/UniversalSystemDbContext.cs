using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Universal.System.Entity.Model;

namespace Universal.System.Entity
{
    /// <summary>
    /// 数据库上下文
    /// </summary>
    public sealed class UniversalSystemDbContext : DbContext
    {
        public UniversalSystemDbContext(DbContextOptions<UniversalSystemDbContext> options) : base(options)
        {

        }

        /// <summary>
        /// 用户表
        /// </summary>
        public DbSet<UserModel> User { get; set; }

        /// <summary>
        /// 角色表
        /// </summary>
        public DbSet<RoleModel> Role { get; set; }

        /// <summary>
        /// 权限表
        /// </summary>
        public DbSet<PermissionsModel> Permissions { get; set; }

        /// <summary>
        /// 菜单表
        /// </summary>
        public DbSet<MenuModel> Menu { get; set; }

        /// <summary>
        /// 角色权限中间表
        /// </summary>
        public DbSet<RolePermissionsModel> RolePermissions { get; set; }

        /// <summary>
        /// 用户权限中间表
        /// </summary>
        public DbSet<UserPermissionsModel> UserPermissions { get; set; }

        /// <summary>
        /// 用户角色中间表
        /// </summary>
        public DbSet<UserRoleModel> UserRole { get; set; }


    }
}
