using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Universal.System.Entity.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sys.PermissionsTB",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PermissionsName = table.Column<string>(maxLength: 20, nullable: false),
                    PermissionsValue = table.Column<string>(maxLength: 100, nullable: false),
                    Remark = table.Column<string>(maxLength: 200, nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    IsDelete = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys.PermissionsTB", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Sys.RoleTB",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleName = table.Column<string>(maxLength: 20, nullable: false),
                    Remark = table.Column<string>(maxLength: 300, nullable: false),
                    RoleStatus = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    IsDelete = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys.RoleTB", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Sys.UserTB",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(maxLength: 50, nullable: false),
                    Password = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    AccountStatus = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    IsDelete = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys.UserTB", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Sys.MenuTB",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MenuName = table.Column<string>(maxLength: 20, nullable: false),
                    MenuUrl = table.Column<string>(maxLength: 100, nullable: false),
                    MenuIcon = table.Column<string>(maxLength: 100, nullable: false),
                    Remark = table.Column<string>(maxLength: 200, nullable: false),
                    ParentID = table.Column<int>(nullable: false),
                    PermissionsID = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    IsDelete = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys.MenuTB", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Sys.MenuTB_Sys.PermissionsTB_PermissionsID",
                        column: x => x.PermissionsID,
                        principalTable: "Sys.PermissionsTB",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sys.RolePermissionsTB",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleID = table.Column<int>(nullable: false),
                    PermissionsID = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    IsDelete = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys.RolePermissionsTB", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Sys.RolePermissionsTB_Sys.PermissionsTB_PermissionsID",
                        column: x => x.PermissionsID,
                        principalTable: "Sys.PermissionsTB",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sys.RolePermissionsTB_Sys.RoleTB_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Sys.RoleTB",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sys.UserPermissionsTB",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserID = table.Column<int>(nullable: false),
                    PermissionsID = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    IsDelete = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys.UserPermissionsTB", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Sys.UserPermissionsTB_Sys.PermissionsTB_PermissionsID",
                        column: x => x.PermissionsID,
                        principalTable: "Sys.PermissionsTB",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sys.UserPermissionsTB_Sys.UserTB_UserID",
                        column: x => x.UserID,
                        principalTable: "Sys.UserTB",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sys.UserRoleTB",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserID = table.Column<int>(nullable: false),
                    RoleID = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    IsDelete = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys.UserRoleTB", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Sys.UserRoleTB_Sys.RoleTB_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Sys.RoleTB",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sys.UserRoleTB_Sys.UserTB_UserID",
                        column: x => x.UserID,
                        principalTable: "Sys.UserTB",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sys.MenuTB_PermissionsID",
                table: "Sys.MenuTB",
                column: "PermissionsID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sys.RolePermissionsTB_PermissionsID",
                table: "Sys.RolePermissionsTB",
                column: "PermissionsID");

            migrationBuilder.CreateIndex(
                name: "IX_Sys.RolePermissionsTB_RoleID",
                table: "Sys.RolePermissionsTB",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_Sys.UserPermissionsTB_PermissionsID",
                table: "Sys.UserPermissionsTB",
                column: "PermissionsID");

            migrationBuilder.CreateIndex(
                name: "IX_Sys.UserPermissionsTB_UserID",
                table: "Sys.UserPermissionsTB",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Sys.UserRoleTB_RoleID",
                table: "Sys.UserRoleTB",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_Sys.UserRoleTB_UserID",
                table: "Sys.UserRoleTB",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sys.MenuTB");

            migrationBuilder.DropTable(
                name: "Sys.RolePermissionsTB");

            migrationBuilder.DropTable(
                name: "Sys.UserPermissionsTB");

            migrationBuilder.DropTable(
                name: "Sys.UserRoleTB");

            migrationBuilder.DropTable(
                name: "Sys.PermissionsTB");

            migrationBuilder.DropTable(
                name: "Sys.RoleTB");

            migrationBuilder.DropTable(
                name: "Sys.UserTB");
        }
    }
}
