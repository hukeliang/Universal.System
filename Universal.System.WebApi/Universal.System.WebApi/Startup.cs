﻿using Universal.System.WebApi.Extensions;
using Universal.System.WebApi.Filter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;

namespace Universal.System.WebApi
{

    /// <summary>
    /// 参考 https://dnczeus.codedefault.com/rbac/user
    /// </summary>
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;

        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // 替换系统默认Controller创建器  ps：必须放到 AddMvc 前面注册
            services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());

            services.AddDbConnectionMaster(Configuration["Connection:ConnectionStrings"]);

            #region 配置跨域处理
            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin", builder =>
                {
                    builder.WithOrigins(Configuration["AllowCors:AllowOrigin"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();//指定处理cookie
                });
            });
            #endregion


            #region Swagger UI API 接口说明文档
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info()
                {
                    Title = "Universal.System.WebApi 接口说明",
                    Version = "v1",
                    Description = "Universal.System.WebApi  ASP.NET Core Web API",
                    TermsOfService = "None",
                    Contact = new Contact
                    {
                        Name = "hukeliang",
                        Email = "732213520@qq,com",
                        //Url = "链接地址"
                    },
                    //License = new License
                    //{
                    //    Name = "许可证名字",
                    //    Url = "链接地址"
                    //}
                });

                options.CustomSchemaIds(type => type.FullName); // 解决相同类名会报错的问题
                options.IncludeXmlComments(Path.Combine(Directory.GetCurrentDirectory(), "SwaggerUI.xml")); // 标注要使用的 XML 文档
            });
            #endregion


            services.AddMvc(options => options.Filters.Add<ValidateTokenAttribute>())
                .AddJsonOptions(options => options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss")
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            return services.AutofacInitialize(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseInternalServerError();
            }

            app.UseCors("AllowOrigin");

            //提供对静态资源的访问
            app.UseStaticFiles();

            app.UseSwagger();
            // 在这里面可以注入
            app.UseSwaggerUI(options =>
            {
                options.InjectJavascript("/swagger/ui/swagger_zh_CN.js"); // 加载中文包
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Universal.System.WebApi API V1");//必须将发布目录的文件夹权限打开（允许写入，读取）
            });


            app.UseMvc();
        }
    }
}
