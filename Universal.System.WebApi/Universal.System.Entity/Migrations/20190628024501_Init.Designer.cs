﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Universal.System.Entity;

namespace Universal.System.Entity.Migrations
{
    [DbContext(typeof(UniversalSystemDbContext))]
    [Migration("20190628024501_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Universal.System.Entity.Model.Menu", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate");

                    b.Property<int>("IsDelete");

                    b.Property<string>("MenuIcon")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("MenuName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("MenuUrl")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("ParentID");

                    b.Property<int>("PermissionsID");

                    b.Property<string>("Remark")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("ID");

                    b.HasIndex("PermissionsID")
                        .IsUnique();

                    b.ToTable("Sys.MenuTB");
                });

            modelBuilder.Entity("Universal.System.Entity.Model.Permissions", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate");

                    b.Property<int>("IsDelete");

                    b.Property<string>("PermissionsName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("PermissionsValue")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Remark")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("ID");

                    b.ToTable("Sys.PermissionsTB");
                });

            modelBuilder.Entity("Universal.System.Entity.Model.Role", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate");

                    b.Property<int>("IsDelete");

                    b.Property<string>("Remark")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<int>("RoleStatus");

                    b.HasKey("ID");

                    b.ToTable("Sys.RoleTB");
                });

            modelBuilder.Entity("Universal.System.Entity.Model.RolePermissions", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate");

                    b.Property<int>("IsDelete");

                    b.Property<int>("PermissionsID");

                    b.Property<int>("RoleID");

                    b.HasKey("ID");

                    b.HasIndex("PermissionsID");

                    b.HasIndex("RoleID");

                    b.ToTable("Sys.RolePermissionsTB");
                });

            modelBuilder.Entity("Universal.System.Entity.Model.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountStatus");

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("IsDelete");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.ToTable("Sys.UserTB");
                });

            modelBuilder.Entity("Universal.System.Entity.Model.UserPermissions", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate");

                    b.Property<int>("IsDelete");

                    b.Property<int>("PermissionsID");

                    b.Property<int>("UserID");

                    b.HasKey("ID");

                    b.HasIndex("PermissionsID");

                    b.HasIndex("UserID");

                    b.ToTable("Sys.UserPermissionsTB");
                });

            modelBuilder.Entity("Universal.System.Entity.Model.UserRole", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate");

                    b.Property<int>("IsDelete");

                    b.Property<int>("RoleID");

                    b.Property<int>("UserID");

                    b.HasKey("ID");

                    b.HasIndex("RoleID");

                    b.HasIndex("UserID");

                    b.ToTable("Sys.UserRoleTB");
                });

            modelBuilder.Entity("Universal.System.Entity.Model.Menu", b =>
                {
                    b.HasOne("Universal.System.Entity.Model.Permissions", "Permissions")
                        .WithOne("Menu")
                        .HasForeignKey("Universal.System.Entity.Model.Menu", "PermissionsID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Universal.System.Entity.Model.RolePermissions", b =>
                {
                    b.HasOne("Universal.System.Entity.Model.Permissions", "Permissions")
                        .WithMany()
                        .HasForeignKey("PermissionsID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Universal.System.Entity.Model.Role", "Roles")
                        .WithMany("RolePermissionsCollection")
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Universal.System.Entity.Model.UserPermissions", b =>
                {
                    b.HasOne("Universal.System.Entity.Model.Permissions", "Permissions")
                        .WithMany("UserPermissionsCollection")
                        .HasForeignKey("PermissionsID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Universal.System.Entity.Model.User", "Users")
                        .WithMany("UserPermissionsCollection")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Universal.System.Entity.Model.UserRole", b =>
                {
                    b.HasOne("Universal.System.Entity.Model.Role", "RoleModel")
                        .WithMany("UserRoleCollection")
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Universal.System.Entity.Model.User", "UserModel")
                        .WithMany("UserRoleCollection")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}